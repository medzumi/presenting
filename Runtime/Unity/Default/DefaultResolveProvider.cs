using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using unityPresenting.Core;

namespace unityPresenting.Unity.Default
{
    [CreateAssetMenu(menuName = "presenting/Default/DefaultResolveProvider")]
    public class DefaultResolveProvider : AbstractResolverProvider, IPresenterResolver, IViewResolver
    {
        [SerializeField] private List<ScriptablePresenterReadableList> _presenterCollections;
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

        public IPresenter<TModel, TView> Resolve<TModel, TView>(string key)
        {
            if (!_presenters.TryGetValue(key, out var presenter))
            {
                foreach (var scriptablePresenterCollection in _presenterCollections)
                {
                    _presenterDataBuffer.Clear();
                    foreach (var presenterData in scriptablePresenterCollection.ReadData(_presenterDataBuffer))
                    {
                        if (string.Equals(presenterData.Key, key))
                        {
                            var concretePresenter = ((IPresenter<TModel,TView>)presenterData.Presenter).Clone();
                            foreach (var injector in _injectors)
                            {
                                injector.Inject(concretePresenter, key);
                            }
                            _presenters[key] = presenter = concretePresenter;
                            break;
                        }
                    }
                }
            }

            return ((IPresenter<TModel, TView>)presenter).Clone();
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

        public List<PresenterData> ReadData(List<PresenterData> presenterData)
        {
            foreach (var scriptablePresenterCollection in _presenterCollections)
            {
                presenterData = scriptablePresenterCollection.ReadData(presenterData);
            }

            return presenterData;
        }

        public List<ViewData> ReadData(List<ViewData> presenterData)
        {
            foreach (var scriptableViewFabric in _scriptableViewFabrics)
            {
                presenterData = scriptableViewFabric.ReadData(presenterData);
            }

            return presenterData;
        }
    }
}