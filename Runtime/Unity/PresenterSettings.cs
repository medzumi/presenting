using Game.CoreLogic;
using Packages.Utilities.Unity.Runtime.ScriptableSingletone;
//using Packages.Utilities.Unity.Runtime.ScriptableSingletone;
using UnityEngine;
using Utilities.GenericPatterns;

namespace Unity
{
    public class PresenterSettings : RuntimeScriptableSingletone<PresenterSettings>
    {
        public const string AssetPath = "Assets/ProjectSettings";

        [SerializeField] private AbstractResolverProvider _abstractResolverProvider;

        [RuntimeInitializeOnLoadMethod]
        private void Initialize()
        {
            SingletoneProvider<IPresenterResolver>.InstanceProvider = _abstractResolverProvider.ProvidePresenterResolver();
            SingletoneProvider<IViewModelResolver>.InstanceProvider =
                _abstractResolverProvider.ProvideViewModelResolver();
        }
        
        private class SingletoneProvider<TProvide> : Singletone<TProvide>
        {
            public static TProvide InstanceProvider
            {
                get => instance;
                set => instance = value;
            }
        }
    }
}