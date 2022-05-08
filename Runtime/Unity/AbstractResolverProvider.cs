﻿using Game.CoreLogic;
using UnityEngine;

namespace Unity
{
    public abstract class AbstractResolverProvider : ScriptableObject
    {
        public abstract IPresenterResolver ProvidePresenterResolver();

        public abstract IViewResolver ProvideViewModelResolver();
    }
}