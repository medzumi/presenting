using ViewModel;

namespace Game.CoreLogic
{
    public interface IPresenterResolver
    {
        public IEcsPresenter Resolve(string key);

        public IEcsPresenter Resolve(string key, out IViewModel viewModel);
    }
}