namespace presenting.Unity.Default
{
    public interface IInjectResolver<TResolver>
    {
        void Inject(TResolver resolver);
    }
}