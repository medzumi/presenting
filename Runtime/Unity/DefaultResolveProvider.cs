using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationScripts.CodeExtensions;
using Game.CoreLogic;
using UnityEngine;
using Utilities.Pooling;
using ViewModel;

namespace Unity
{
    [CreateAssetMenu]
    public class DefaultResolveProvider : AbstractResolverProvider, IPresenterResolver, IViewModelResolver
    {
        [SerializeField] private DefaultPresenterConfig _defaultPresenterConfig;
        [SerializeField] private List<MonoViewModel> _monoViewModels;

        private readonly Dictionary<string, IEcsPresenter> _ecsPresenters = new Dictionary<string, IEcsPresenter>();

        private readonly Dictionary<string, IConcreteResolver> _monoViewModelPool =
            new Dictionary<string, IConcreteResolver>();

        private readonly List<IEcsPresenter> _buffer = new List<IEcsPresenter>();

        public override IPresenterResolver ProvidePresenterResolver()
        {
            return this;
        }

        public override IViewModelResolver ProvideViewModelResolver()
        {
            return this;
        }

        public List<string> GetViewModelsKeys(List<string> buffer = null)
        {
            if (buffer.IsNull())
            {
                buffer = new List<string>();
            }

            buffer.AddRange(_monoViewModels.Select(model => model.name));
            return buffer;
        }

        IViewModel IViewModelResolver.Resolve(string key)
        {
            return GetResolver(key).Resolve();
        }


        public IConcreteResolver GetResolver(string key)
        {
            if (!_monoViewModelPool.TryGetValue(key, out var concreteResolver))
            {
                var exampleMonoViewModel = _monoViewModels.FirstOrDefault(model => string.Equals(model.name, key));
                if (!exampleMonoViewModel)
                    return null;
                _monoViewModelPool[key] = concreteResolver = new ConcreteResolver(exampleMonoViewModel);
            }

            return concreteResolver;
        }

        public List<string> GetPresentersKeys(List<string> buffer)
        {
            if (buffer.IsNull())
            {
                buffer = new List<string>();
            }

            foreach (var ecsPresenterConfig in _defaultPresenterConfig.GetEcsPresenterConfigs())
            {
                buffer.Add(ecsPresenterConfig.GetKey());
            }

            return buffer;
        }

        IEcsPresenter IPresenterResolver.Resolve(string key)
        {
            if (!_ecsPresenters.TryGetValue(key, out var presenter))
            {
                var config = _defaultPresenterConfig.GetEcsPresenterConfigs()
                    .FirstOrDefault(config => string.Equals(key, config.GetKey()));
                
                _ecsPresenters[key] = presenter = config.GetPresenter();
            }

            return presenter.Clone();
        }
        
        
        private class ConcreteResolver : IConcreteResolver
        {
            private readonly Pool<MonoViewModel> _viewModelPool;
            private Transform _rootTransform;

            public ConcreteResolver(MonoViewModel monoViewModel)
            {
                _viewModelPool = new Pool<MonoViewModel>(0, () =>
                {
                    if (!_rootTransform)
                    {
                        _rootTransform = (new GameObject($"Root of {monoViewModel.name}")).transform;
                    }

                    var result = Instantiate(monoViewModel, _rootTransform);
                    return result;
                }, model =>
                {
                    var disposer = MonoViewModelDisposer.Create();
                    disposer.Pool = _viewModelPool;
                    disposer.MonoViewModel = model;
                    model.AddTo(disposer);
                }, model =>
                {
                    if (!_rootTransform)
                    {
                        _rootTransform = new GameObject($"Root of {monoViewModel.name}").transform;
                    }
                    model.gameObject.SetActive(false);
                    model.transform.SetParent(_rootTransform);
                });
            }
            
            public IViewModel Resolve()
            {
                return _viewModelPool.Get();
            }
        }
        
        private class MonoViewModelDisposer : PoolableObject<MonoViewModelDisposer>
        {
            public MonoViewModel MonoViewModel;
            public Pool<MonoViewModel> Pool;

            protected override void DisposeHandler()
            {
                base.DisposeHandler();
                if (MonoViewModel)
                {
                    Pool.Release(MonoViewModel);
                }

                MonoViewModel = null;
                Pool = null;
            }
        }
    }
}