using System;
using System.Collections.Generic;

namespace Game.CoreLogic
{
    public interface IPresenterResolver
    {
        public IEcsPresenter<TModel, TView> Resolve<TModel, TView>(string key);
        
        List<string> GetPresentersKeys<TModel, TView>(List<string> buffer);

        List<string> GetPresentersKeys(List<string> buffer, Type modelType, Type viewType);
    }
}