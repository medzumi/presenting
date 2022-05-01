using System;
using System.Collections.Generic;
using Game.CoreLogic;
using UnityEngine;
using ViewModel;

namespace Editor
{
    public class MockMonoViewModelProvider : IViewModelResolver
    {
        private readonly IViewModelResolver _viewModelResolver;

        public MockMonoViewModelProvider(IViewModelResolver viewModelResolver)
        {
            _viewModelResolver = viewModelResolver;
        }
        
        public List<string> GetViewModelsKeys(List<string> buffer = null)
        {
            return _viewModelResolver.GetViewModelsKeys(buffer);
        }

        public IViewModel Resolve(string key)
        {
            try
            {
                return _viewModelResolver.Resolve(key);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }

        public IConcreteResolver GetResolver(string key)
        {
            try
            {
                return _viewModelResolver.GetResolver(key);
            }
            catch(Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }
    }
}