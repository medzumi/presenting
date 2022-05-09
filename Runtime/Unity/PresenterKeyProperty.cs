using System;
using UnityEngine;

namespace unityPresenting.Unity
{
    public class PresenterKeyProperty : PropertyAttribute
    {
        public readonly Type ModelType;
        public readonly Type ViewType;

        public PresenterKeyProperty(Type modelType, Type viewType)
        {
            ModelType = modelType;
            ViewType = viewType;
        }
    }
}