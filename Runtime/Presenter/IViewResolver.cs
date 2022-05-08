using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Pooling;
using ViewModel;

namespace Game.CoreLogic
{
    public interface IViewResolver
    {
        public List<string> GetViewModelsKeys<TView>(List<string> buffer);
        
        public List<string> GetViewModelsKeys(List<string> buffer, Type viewType);

        public TView Resolve<TView>(string key);

        public IConcreteResolver<TView> GetResolver<TView>(string key);
    }

    public interface IConcreteResolver<TView>
    {
        TView Resolve();
    }

}