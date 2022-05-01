using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Pooling;
using ViewModel;

namespace Game.CoreLogic
{
    public interface IViewModelResolver
    {
        public List<string> GetViewModelsKeys(List<string> buffer = null);

        public IViewModel Resolve(string key);

        public IConcreteResolver GetResolver(string key);
    }

    public interface IConcreteResolver
    {
        IViewModel Resolve();
    }

}