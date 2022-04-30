using System.Collections.Generic;

namespace Game.CoreLogic
{
    public interface IPresenterResolver
    {
        public IEcsPresenter Resolve(string key);
        List<string> GetPresentersKeys(List<string> buffer);
    }
}