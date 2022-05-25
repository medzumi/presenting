using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using unityPresenting.Core;
using Utilities;
using Utilities.Pooling;

namespace unityPresenting.Unity.Default
{
    [CreateAssetMenu(menuName = "presenting/Default/DefaultResolveProvider")]
    public class DefaultResolveProvider : AbstractResolverProvider, IPresenterResolver, IViewResolver
    {
        [SerializeField] private AbstractPresentersContainer abstractPresentersContainer;
        [FormerlySerializedAs("_injector")] [SerializeField] private List<AbstractInjector> _injectors;
        [SerializeField] private List<ScriptableViewFabric> _scriptableViewFabrics = new List<ScriptableViewFabric>();

        private Dictionary<string, IPresenter> _presenters = new Dictionary<string, IPresenter>();
        private readonly List<PresenterData> _presenterDataBuffer = new List<PresenterData>();
        private readonly List<ViewData> _viewDatasBuffer = new List<ViewData>();

        public override IPresenterResolver ProvidePresenterResolver()
        {
            return this;
        }

        public override IViewResolver ProvideViewResolver()
        {
            return this;
        }

        public override IPresentersContainer ProvideContainerPresenterRegistrator()
        {
            return abstractPresentersContainer;
        }

        public IPresenter<TModel, TView> Resolve<TModel, TView>(string key)
        {
            if (!_presenters.TryGetValue(key, out var presenter))
            {
                presenter = abstractPresentersContainer.GetPresenterByKey(key);
                presenter = presenter.Clone();
                foreach (var abstractInjector in _injectors)
                {
                    abstractInjector.Inject(presenter, key);
                }
                if (presenter is IPresenter<TModel, TView> genericPresenter)
                {
                    _presenters[key] = genericPresenter;
                }
                else
                {
                    var resolve = GenericPresenterResolver<TModel, TView>.Create();
                    resolve._noneGenericPresenter = presenter;
                    presenter = _presenters[key] = resolve;
                }
            }

            return (presenter.Clone()) as IPresenter<TModel, TView>;
        }


        public TView Resolve<TView>(string key)
        {
            //ToDo : fix it please
            foreach (var viewFabric in _scriptableViewFabrics)
            {
                _viewDatasBuffer.Clear();
                foreach (var viewData in viewFabric.ReadData(_viewDatasBuffer))
                {
                    if (string.Equals(viewData.Key, key) && typeof(TView).IsAssignableFrom(viewData.view.GetType()))
                    {
                        return (TView)viewFabric.Create(key); 
                    }
                }
            }

            return default;
        }

        public List<ViewData> ReadData(List<ViewData> presenterData)
        {
            foreach (var scriptableViewFabric in _scriptableViewFabrics)
            {
                presenterData = scriptableViewFabric.ReadData(presenterData);
            }

            return presenterData;
        }
        
        private class GenericPresenterResolver<TModel, TView> : PoolableObject<GenericPresenterResolver<TModel, TView>>, IPresenter<TModel, TView>
        {
            public IPresenter _noneGenericPresenter;
                        
            public void Initialize(TModel ecsPresenterData, TView tView)
            {
                _noneGenericPresenter.Initialize(ecsPresenterData, tView);
            }

            IPresenter IClonable<IPresenter>.Clone()
            {
                var clone = Create();
                clone._noneGenericPresenter = _noneGenericPresenter.Clone();
                return clone;
            }

            public Type GetModelType()
            {
                return _noneGenericPresenter.GetModelType();
            }

            public Type GetViewType()
            {
                return _noneGenericPresenter.GetViewType();
            }

            public void Initialize(object model, object view)
            {
                _noneGenericPresenter.Initialize(model, view);
            }

            IPresenter<TModel, TView> IClonable<IPresenter<TModel, TView>>.Clone()
            {
                var clone = Create();
                clone._noneGenericPresenter = _noneGenericPresenter.Clone();
                return clone;
            }

            protected override void DisposeHandler()
            {
                _noneGenericPresenter.Dispose();
                _noneGenericPresenter = null;
                base.DisposeHandler();
            }
        }
    }
}