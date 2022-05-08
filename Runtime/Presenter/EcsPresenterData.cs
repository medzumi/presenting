using System;
using Leopotam.EcsLite;
using ViewModel;

namespace Game.CoreLogic
{
    [Serializable]
    public class EcsPresenterData
    {
        public EcsWorld ModelWorld;
        public int ModelEntity;
    }
}