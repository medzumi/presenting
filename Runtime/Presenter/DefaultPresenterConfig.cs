using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.CoreLogic
{
    [CreateAssetMenu]
    [Serializable]
    public class DefaultPresenterConfig : AbstractEcsConfigs
    {
        [SerializeField] private List<PresenterConfig> _presenter;

        public override List<IEcsPresenterConfig> GetEcsPresenterConfigs()
        {
            return _presenter.Select(p => (IEcsPresenterConfig)p).ToList();
        }
    }
}