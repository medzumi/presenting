﻿using UnityEngine;
using unityPresenting.Core;

namespace unityPresenting.Unity
{
    public abstract class AbstractResolverProvider : ScriptableObject
    {
        public abstract IPresenterResolver ProvidePresenterResolver();

        public abstract IViewResolver ProvideViewResolver();

        public abstract IPresentersContainer ProvideContainerPresenterRegistrator();
    }
}