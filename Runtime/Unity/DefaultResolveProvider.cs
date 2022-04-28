using Game.CoreLogic;
using ViewModel;

namespace Unity
{
    public class DefaultResolveProvider : AbstractResolverProvider
    {
        private class DefaultPresenterResolver : IPresenterResolver
        {
            public IEcsPresenter Resolve(string key)
            {
                throw new System.NotImplementedException();
            }

            public IEcsPresenter Resolve(string key, out IViewModel viewModel)
            {
                throw new System.NotImplementedException();
            }
        }

        public override IPresenterResolver ProvideResolver()
        {
            throw new System.NotImplementedException();
        }
    }
}