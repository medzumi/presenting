using System;
using System.Collections.Generic;
using UnityEngine;
using unityPresenting.Core;

namespace unityPresenting.Unity
{
    public abstract class ScriptableViewFabric : ScriptableObject, IReadableList<ViewData>
    {
        public abstract Type GetFabricType();

        public abstract object Create(string key);
        public abstract List<ViewData> ReadData(List<ViewData> presenterData);
    }

    public abstract class ScriptableViewFabric<TView> : ScriptableViewFabric
    {
        public sealed override Type GetFabricType()
        {
            return typeof(TView);
        }
    }
}