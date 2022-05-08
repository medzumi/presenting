using System;
using UnityEngine;

namespace Unity
{
    public class ViewKeyProperty : PropertyAttribute
    {
        public readonly Type ViewType;

        public ViewKeyProperty(Type viewType)
        {
            ViewType = viewType;
        }
    }

    public class ViewDataKeyProperty : PropertyAttribute
    {
    }

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