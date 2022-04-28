using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities.SerializeReferencing;
using ViewModel;

namespace Game.CoreLogic
{
    public interface IPresenterResolver
    {
        public IEcsPresenter Resolve(string key);

        public IEcsPresenter Resolve(string key, out IViewModel viewModel);
    }

    public abstract class AbstractEcsConfigs : ScriptableObject
    {
        public abstract List<IEcsPresenterConfig> GetEcsPresenterConfigs();
    }

    [CreateAssetMenu]
    [Serializable]
    public class TestEcsConfigs : AbstractEcsConfigs
    {
        [SerializeField] private List<PresenterConfig> _presenter;

        public override List<IEcsPresenterConfig> GetEcsPresenterConfigs()
        {
            return _presenter.Select(p => (IEcsPresenterConfig)p).ToList();
        }
    }

    [Serializable]
    public class PresenterConfig : IEcsPresenterConfig
    {
        [SerializeField] private string _key;
        [SerializeField] private string _viewModelKey;

        [SerializeReference] [SerializeTypes(typeof(IEcsPresenter))]
        private List<IEcsPresenter> _presenters;

        public string GetKey()
        {
            return _key;
        }

        public List<IEcsPresenter> GetEcsPresenters()
        {
            return _presenters;
        }

        public string GetViewModelKey()
        {
            return _viewModelKey;
        }
    }

    public interface IEcsPresenterConfig
    {
        string GetKey();

        List<IEcsPresenter> GetEcsPresenters();

        string GetViewModelKey();
    }
}