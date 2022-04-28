using Game.CoreLogic;
//using Packages.Utilities.Unity.Runtime.ScriptableSingletone;
using UnityEngine;
using Utilities.GenericPatterns;

namespace Unity
{
    public class PresenterSettings //: RuntimeScriptableSingletone<PresenterSettings>
    {
        public const string AssetPath = "Assets/ProjectSettings";

        [SerializeField] private AbstractResolverProvider _abstractResolverProvider;

        [RuntimeInitializeOnLoadMethod]
        private void Initialize()
        {
            SingletoneProvider.InstanceProvider = _abstractResolverProvider.ProvideResolver();
        }
        
        private class SingletoneProvider : Singletone<IPresenterResolver>
        {
            public static IPresenterResolver InstanceProvider
            {
                get => instance;
                set => instance = value;
            }
        }
    }
}