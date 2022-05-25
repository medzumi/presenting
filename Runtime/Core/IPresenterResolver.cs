namespace unityPresenting.Core
{
    public interface IPresenterResolver
    {
        public IPresenter<TModel, TView> Resolve<TModel, TView>(string key);
    }
}