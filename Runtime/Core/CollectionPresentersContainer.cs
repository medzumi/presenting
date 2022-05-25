using System.Collections.Generic;
using UnityEngine;
using Utilities.Unity.Extensions;

namespace unityPresenting.Core
{
    [CreateAssetMenu]
    public class CollectionPresentersContainer : AbstractPresentersContainer
    {
        [SerializeField] private List<AbstractPresentersContainer> _presenterRegistrators;
        public override IPresenter GetPresenterByKey(string key)
        {
            foreach (var abstractPresentersRegistrator in _presenterRegistrators)
            {
                var presenter = abstractPresentersRegistrator.GetPresenterByKey(key);
                if (presenter.IsNullInUnity())
                {
                    return presenter;
                }
            }

            return null;
        }

        public override string GetPresenterNameByKey(string key)
        {
            foreach (var abstractPresentersRegistrator in _presenterRegistrators)
            {
                var name = abstractPresentersRegistrator.GetPresenterNameByKey(key);
                if (name != null)
                {
                    return name;
                }
            }

            return null;
        }

        public override List<PresenterSource> ReadPresenterRegistrators(List<PresenterSource> buffer)
        {
            foreach (var abstractPresenterRegistrator in _presenterRegistrators)
            {
                abstractPresenterRegistrator.ReadPresenterRegistrators(buffer);
            }

            return buffer;
        }
    }
}