using System.Collections.Generic;
using UnityEngine;

namespace Game.CoreLogic
{
    public abstract class AbstractEcsConfigs : ScriptableObject
    {
        public abstract List<IEcsPresenterConfig> GetEcsPresenterConfigs();
    }
}