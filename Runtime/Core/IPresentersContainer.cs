using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace unityPresenting.Core
{
    public interface IPresentersContainer
    {
        IPresenter GetPresenterByKey(string key);

        string GetPresenterNameByKey(string key);
        
        List<PresenterSource> ReadPresenterRegistrators(List<PresenterSource> buffer);
    }
}