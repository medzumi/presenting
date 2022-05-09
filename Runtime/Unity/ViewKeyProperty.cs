using System;
using UnityEngine;

namespace unityPresenting.Unity
{
    public class ViewKeyProperty : PropertyAttribute
    {
        public readonly Type ViewType;

        public ViewKeyProperty(Type viewType)
        {
            ViewType = viewType;
        }
    }
}