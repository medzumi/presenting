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

        public IViewModelResolver ViewModelResolver => _abstractResolverProvider.ProvideViewModelResolver();
        public IPresenterResolver PresenterResolver => _abstractResolverProvider.ProvidePresenterResolver();
    }
}