using UnityEngine;

namespace unityPresenting.Unity
{
    public abstract class AbstractInjector : ScriptableObject
    {
        public abstract void Inject(object obj, string key);

        public abstract void Inject<TObject>(TObject obj, string key);
    }
}