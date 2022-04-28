using System.Collections.Generic;
using UnityEngine;
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

    public interface IEcsPresenterConfig
    {
        string GetKey();

        List<IEcsPresenter> GetEcsPresenters();

        string GetViewModelKey();
    }
}