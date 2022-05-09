using System.Collections.Generic;

namespace unityPresenting.Core
{
    public interface IReadableList<TReadableType>
    {
        List<TReadableType> ReadData(List<TReadableType> presenterData);
    }
}