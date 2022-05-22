using presenting.Unity.Default;
using UnityEngine;
using unityPresenting.Core;

namespace unityPresenting.Unity
{
    [CreateAssetMenu(menuName = "presenting/Default/DefaultInjector")]
    public class DefaultInjector : AbstractInjector
    {
        [SerializeField] private AbstractResolverProvider _abstractResolverProvider;

        public override void Inject(object obj, string key)
        {
            if (obj is IInject<IPresenterResolver> injectResolver)
            {
                injectResolver.Inject(_abstractResolverProvider.ProvidePresenterResolver());
            }

            if (obj is IInject<IViewResolver> injectViewResolver)
            {
                injectViewResolver.Inject(_abstractResolverProvider.ProvideViewResolver());
            }
        }

        public override void Inject<TObject>(TObject obj, string key)
        {
            if (obj is IInject<IPresenterResolver> injectResolver)
            {
                injectResolver.Inject(_abstractResolverProvider.ProvidePresenterResolver());
            }

            if (obj is IInject<IViewResolver> injectViewResolver)
            {
                injectViewResolver.Inject(_abstractResolverProvider.ProvideViewResolver());
            }
        }
    }
}