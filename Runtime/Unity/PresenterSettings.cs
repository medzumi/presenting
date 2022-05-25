using UnityEngine;
using unityPresenting.Core;
using Utilities.Unity.Extensions;
using Utilities.Unity.ScriptableSingletone;

//using Packages.Utilities.Unity.Runtime.ScriptableSingletone;

namespace unityPresenting.Unity
{
    public class PresenterSettings : RuntimeScriptableSingletone<PresenterSettings>
    {
        public const string AssetPath = "Assets/ProjectSettings";

        [SerializeField] private AbstractResolverProvider _abstractResolverProvider;

        private IViewResolver _viewResolver;
        private IPresenterResolver _presenterResolver;
        private IPresentersContainer _presentersContainer;

        public IViewResolver ViewResolver
        {
            get
            {
                if (_viewResolver.IsNullInUnity())
                {
                    _viewResolver = _abstractResolverProvider.ProvideViewResolver();
                }

                return _viewResolver;
            }
        }

        public IPresenterResolver PresenterResolver
        {
            get
            {
                if (_presenterResolver.IsNullInUnity())
                {
                    _presenterResolver = _abstractResolverProvider.ProvidePresenterResolver();
                }

                return _presenterResolver;
            }
        }

        public IPresentersContainer PresentersContainer
        {
            get
            {
                if (_presentersContainer.IsNullInUnity())
                {
                    _presentersContainer = _abstractResolverProvider.ProvideContainerPresenterRegistrator();
                }

                return _presentersContainer;
            }
        }
    }
}