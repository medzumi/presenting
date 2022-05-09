using System;
using UnityEngine;
using unityPresenting.Core;
using Utilities.Unity.SerializeReferencing;

namespace unityPresenting.Unity
{
    [Serializable]
    public class PresenterConfig
    {
        [SerializeField] private string _key;

        [SerializeReference] [SerializeTypes(typeof(IPresenter))] private IPresenter _presenter = null;

        public string GetKey()
        {
            return _key;
        }

        public IPresenter GetPresenter()
        {
            return _presenter;
        }
    }
}