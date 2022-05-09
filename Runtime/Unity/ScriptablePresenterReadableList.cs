using System.Collections.Generic;
using UnityEngine;
using unityPresenting.Core;

namespace unityPresenting.Unity
{
    public abstract class ScriptablePresenterReadableList : ScriptableObject, IReadableList<PresenterData>
    {
        public abstract List<PresenterData> ReadData(List<PresenterData> presenterData);
    }
}