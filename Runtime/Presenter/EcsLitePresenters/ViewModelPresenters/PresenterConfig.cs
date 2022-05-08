using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.SerializeReferencing;

namespace Game.CoreLogic
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

    public interface IPresenter
    {
        
    }
}