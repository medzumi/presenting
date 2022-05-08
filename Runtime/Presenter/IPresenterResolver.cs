using System;
using System.Collections.Generic;
using Presenter;

namespace Game.CoreLogic
{
    public interface IPresenterResolver : IPresenterCollection
    {
        public IEcsPresenter<TModel, TView> Resolve<TModel, TView>(string key);
        
        List<string> GetPresentersKeys<TModel, TView>(List<string> buffer);

        List<string> GetPresentersKeys(List<string> buffer, Type modelType, Type viewType);
    }
}