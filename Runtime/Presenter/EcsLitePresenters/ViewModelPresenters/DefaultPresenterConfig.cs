using System;
using System.Collections.Generic;
using System.Linq;
using Presenter;
using UnityEngine;
using ViewModel;

namespace Game.CoreLogic
{
    [CreateAssetMenu]
    [Serializable]
    public class DefaultPresenterConfig : ScriptableObject
    {
        [SerializeField] private List<PresenterConfig> _presenter;

        public List<IEcsPresenterConfig<EcsPresenterData, IViewModel>> GetEcsPresenterConfigs()
        {
            return _presenter.Select(p => (IEcsPresenterConfig<EcsPresenterData,IViewModel>)p).ToList();
        }
        
        [ContextMenu("Test")]
        private void Test()
        {
            Debug.Log(typeof(IEcsPresenter<object, object>).IsAssignableFrom(typeof(IEcsPresenter<EcsPresenterData, IViewModel>)));
            Debug.Log(typeof(OpenPresenter).IsAssignableFrom(typeof(IEcsPresenter<,>)));
        }
    }
}