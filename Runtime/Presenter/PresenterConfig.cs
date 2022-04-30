using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.SerializeReferencing;

namespace Game.CoreLogic
{
    [Serializable]
    public class PresenterConfig : IEcsPresenterConfig
    {
        [SerializeField] private string _key;

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
    }
}