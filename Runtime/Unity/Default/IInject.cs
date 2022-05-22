namespace presenting.Unity.Default
{
    public interface IInject<TInjectable>
    {
        void Inject(TInjectable injectable);
    }
}