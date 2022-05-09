namespace unityPresenting.Core
{
    public interface IViewResolver : IReadableList<ViewData>
    {
        public TView Resolve<TView>(string key);
    }

    public struct ViewData
    {
        public object view;
        public string Key;
    }
}