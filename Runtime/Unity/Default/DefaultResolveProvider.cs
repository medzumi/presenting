using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using unityPresenting.Core;

namespace unityPresenting.Unity.Default
{
    [CreateAssetMenu(menuName = "unityPresenting/Default/DefaultResolveProvider")]
    public class DefaultResolveProvider : AbstractResolverProvider, IPresenterResolver, IViewResolver
    {
        [SerializeField] private List<ScriptablePresenterReadableList> _presenterCollections;
        [SerializeField] private AbstractInjector _injector;
        [SerializeField] private List<ScriptableViewFabric> _scriptableViewFabrics = new List<ScriptableViewFabric>();

        private Dictionary<string, IPresenter> _presenters = new Dictionary<string, IPresenter>();
        private readonly List<PresenterData> _presenterDataBuffer = new List<PresenterData>();

        public override IPresenterResolver ProvidePresenterResolver()
        {
            return this;
        }

        public override IViewResolver ProvideViewModelResolver()
        {
            return this;
        }

        public IPresenter<TModel, TView> Resolve<TModel, TView>(string key)
        {
            if (_presenters.TryGetValue(key, out var presenter))
            {
                foreach (var scriptablePresenterCollection in _presenterCollections)
                {
                    _presenterDataBuffer.Clear();
                    foreach (var presenterData in scriptablePresenterCollection.ReadPresenterData(_presenterDataBuffer))
                    {
                        if (string.Equals(presenterData.Key, key))
                        {
                            var concretePresenter = ((IPresenter<TModel,TView>)presenterData.Presenter).Clone();
                            if (_injector)
                            {
                                _injector.Inject(concretePresenter, key);
                            }
                            _presenters[key] = presenter = concretePresenter;
                        }                        
                    }
                }
            }

            return ((IPresenter<TModel, TView>)presenter).Clone();
        }

        public TView Resolve<TView>(string key)
        {
            var fabric =
                _scriptableViewFabrics.First(viewFabric => typeof(TView).IsAssignableFrom(viewFabric.GetFabricType()));
            return (TView)fabric.Create(key);
        }

        public List<PresenterData> ReadPresenterData(List<PresenterData> presenterData)
        {
            foreach (var scriptablePresenterCollection in _presenterCollections)
            {
                presenterData = scriptablePresenterCollection.ReadPresenterData(presenterData);
            }

            return presenterData;
        }

        public List<ViewData> ReadPresenterData(List<ViewData> presenterData)
        {
            foreach (var scriptableViewFabric in _scriptableViewFabrics)
            {
                presenterData = scriptableViewFabric.ReadPresenterData(presenterData);
            }

            return presenterData;
        }
    }
}