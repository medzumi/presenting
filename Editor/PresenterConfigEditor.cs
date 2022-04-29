using System;
using System.Collections.Generic;
using Game.CoreLogic;
using UnityEditor;
using UnityEngine;
using ViewModel;

namespace Editor
{
    [CustomEditor(typeof(AbstractEcsConfigs), true)]
    public class PresenterConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Validate"))
            {
                var config = target as AbstractEcsConfigs;
                foreach (var variable in config.GetEcsPresenterConfigs())
                {
                    
                }
            }
        }
    }

    public class MockMonoViewModelProvider : IViewModelResolver
    {
        private readonly IViewModelResolver _viewModelResolver;

        public MockMonoViewModelProvider(IViewModelResolver viewModelResolver)
        {
            _viewModelResolver = viewModelResolver;
        }
        
        public List<string> GetViewModelKeys(List<string> buffer = null)
        {
            return _viewModelResolver.GetViewModelKeys(buffer);
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
    }
    
    public class MockViewModel : IViewModel
    {
        private readonly IViewModel _originViewModel;

        public MockViewModel(IViewModel viewModel)
        {
            _originViewModel = viewModel;
        }
        
        public T GetViewModelData<T>(string propertyName) where T : IViewModelData
        {
            try
            {
                return _originViewModel.GetViewModelData<T>(propertyName);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return Activator.CreateInstance<T>();
        }

        public T AddTo<T>(T disposable) where T : IDisposable
        {
            _originViewModel.AddTo(disposable);
            return disposable;
        }
    }
}