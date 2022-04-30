using System.Collections.Generic;
using ViewModel;

namespace Game.CoreLogic
{
    public interface IViewModelResolver
    {
        public List<string> GetViewModelsKeys(List<string> buffer = null);

        public IViewModel Resolve(string key);
    }
}