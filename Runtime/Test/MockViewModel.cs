using System;
using UnityEngine;
using ViewModel;

namespace Editor
{
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

        public void SetViewModel(IViewModel viewModel, string key)
        {
            try
            {
                if (viewModel is MockViewModel mockViewModel)
                {
                    viewModel = mockViewModel._originViewModel;
                }
                _originViewModel.SetViewModel(viewModel, key);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void Dispose()
        {
            _originViewModel?.Dispose();
        }
    }
}