using UnityEngine;
using unityPresenting.Core;
using Utilities.Unity.ScriptableSingletone;

//using Packages.Utilities.Unity.Runtime.ScriptableSingletone;

namespace unityPresenting.Unity
{
    public class PresenterSettings : RuntimeScriptableSingletone<PresenterSettings>
    {
        public const string AssetPath = "Assets/ProjectSettings";

        [SerializeField] private AbstractResolverProvider _abstractResolverProvider;

        public IViewResolver ViewResolver => _abstractResolverProvider.ProvideViewModelResolver();
        public IPresenterResolver PresenterResolver => _abstractResolverProvider.ProvidePresenterResolver();
    }
}