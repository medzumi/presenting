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

        [SerializeReference] [SerializeTypes(typeof(IEcsPresenter<,>))] private object _presenter = null;

        public string GetKey()
        {
            return _key;
        }
    }
}