using System.Collections.Generic;

namespace unityPresenting.Core
{
    public interface IReadableList<TReadableType>
    {
        List<TReadableType> ReadPresenterData(List<TReadableType> presenterData);
    }
}