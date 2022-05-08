using System;
using System.Collections.Generic;
using Game.CoreLogic;
using UnityEngine;
using ViewModel;

namespace Editor
{
    public class MockMonoViewProvider : IViewResolver
    {
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
    }
}