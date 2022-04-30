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

        private readonly Dictionary<string, (Transform, Pool<MonoViewModel>)> _monoViewModelPool =
            new Dictionary<string, (Transform, Pool<MonoViewModel>)>();

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
            if (!_monoViewModelPool.TryGetValue(key, out var valueTuple))
            {
                var exampleMonoViewModel = _monoViewModels.FirstOrDefault(model => string.Equals(model.name, key));
                if (!exampleMonoViewModel)
                    return null;
                var poolRoot = (new GameObject($"Pool of {key}")).transform;
                _monoViewModelPool[key] = valueTuple = (poolRoot, new Pool<MonoViewModel>(0, () =>
                {
                    var instance = Instantiate(exampleMonoViewModel, poolRoot);
                    instance.gameObject.SetActive(false);
                    return instance;
                }));
            }

            MonoViewModel resultMonoViewModel = valueTuple.Item2.Get();
            while (!resultMonoViewModel)
            {
                resultMonoViewModel = valueTuple.Item2.Get();
            }

            var disposer = MonoViewModelDisposer.Create();
            disposer.Pool = valueTuple.Item2;
            disposer.Root = valueTuple.Item1;
            disposer.MonoViewModel = resultMonoViewModel;
            resultMonoViewModel.AddTo(disposer);
            return resultMonoViewModel;
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
            
                _buffer.Clear();
                if (config != null)
                {
                    foreach (var ecsPresenter in config.GetEcsPresenters())
                    {
                        _buffer.Add(ecsPresenter.Clone());
                    }
                }

                _ecsPresenters[key] = presenter = new AggregatePresenter(_buffer);
            }

            return presenter.Clone();
        }
        
        private class MonoViewModelDisposer : PoolableObject<MonoViewModelDisposer>
        {
            public MonoViewModel MonoViewModel;
            public Pool<MonoViewModel> Pool;
            public Transform Root;

            protected override void DisposeHandler()
            {
                base.DisposeHandler();
                if (MonoViewModel)
                {
                    MonoViewModel.gameObject.SetActive(false);
                    MonoViewModel.transform.SetParent(Root);
                    Pool.Release(MonoViewModel);
                }

                MonoViewModel = null;
                Pool = null;
            }
        }
    }
}