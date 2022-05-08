using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CoreLogic
{
    public abstract class AbstractConfigs : ScriptableObject
    {
        public abstract Type ModelType { get; }
        public abstract Type ViewType { get; }
    }
    
    public abstract class AbstractConfigs<TModel, TView> : AbstractConfigs
    {
        public override Type ModelType => typeof(TModel);
        public override Type ViewType => typeof(TView);

        public abstract List<IEcsPresenterConfig<TModel, TView>> GetEcsPresenterConfigs();
    }
}