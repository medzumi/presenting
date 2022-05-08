using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationScripts.CodeExtensions;
using Game.CoreLogic;
using Presenter;
using UnityEngine;
using Utilities.Pooling;
using ViewModel;

namespace Unity
{
    [CreateAssetMenu]
    public class DefaultResolveProvider : AbstractResolverProvider, IPresenterResolver, IViewResolver
    {
        [SerializeField] private List<ScriptablePresenterCollection> _presenterCollections;

        public override IPresenterResolver ProvidePresenterResolver()
        {
            return this;
        }

        public override IViewResolver ProvideViewModelResolver()
        {
            throw new NotImplementedException();
        }

        public IEcsPresenter<TModel, TView> Resolve<TModel, TView>(string key)
        {
            throw new NotImplementedException();
        }

        public List<string> GetPresentersKeys<TModel, TView>(List<string> buffer)
        {
            throw new NotImplementedException();
        }

        public List<string> GetPresentersKeys(List<string> buffer, Type modelType, Type viewType)
        {
            throw new NotImplementedException();
        }

        public List<string> GetViewModelsKeys(List<string> buffer = null)
        {
            throw new NotImplementedException();
        }

        public List<string> GetViewModelsKeys<TView>(List<string> buffer)
        {
            throw new NotImplementedException();
        }

        public List<string> GetViewModelsKeys(List<string> buffer, Type viewType)
        {
            throw new NotImplementedException();
        }

        public TView Resolve<TView>(string key)
        {
            throw new NotImplementedException();
        }

        public IConcreteResolver<TView> GetResolver<TView>(string key)
        {
            throw new NotImplementedException();
        }

        public List<PresenterData> ReadPresenterData(List<PresenterData> presenterData)
        {
            foreach (var scriptablePresenterCollection in _presenterCollections)
            {
                presenterData = scriptablePresenterCollection.ReadPresenterData(presenterData);
            }

            return presenterData;
        }
    }
}