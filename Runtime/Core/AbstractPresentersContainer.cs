using System.Collections.Generic;
using UnityEngine;

namespace unityPresenting.Core
{
    public abstract class AbstractPresentersContainer : ScriptableObject, IPresentersContainer
    {
        public abstract IPresenter GetPresenterByKey(string key);
        public abstract string GetPresenterNameByKey(string key);
        public abstract List<PresenterSource> ReadPresenterRegistrators(List<PresenterSource> buffer);
    }
}