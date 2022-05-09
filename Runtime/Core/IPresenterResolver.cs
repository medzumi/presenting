namespace unityPresenting.Core
{
    public interface IPresenterResolver : IReadableList<PresenterData>
    {
        public IPresenter<TModel, TView> Resolve<TModel, TView>(string key);
    }
}