using System;
using System.Collections.Generic;
using UnityEngine;
using unityPresenting.Unity;
using Utilities.Unity.SerializeReferencing;

namespace unityPresenting.Core
{
    [CreateAssetMenu]
    public class ConcretePresenterContainer : AbstractPresentersContainer
    {
        [SerializeField] private string _presenterName;
        [SerializeField][GuidStringKey] private string _key = Guid.NewGuid().ToString();

        [SerializeReference] [SerializeTypes(typeof(IPresenter))]
        private IPresenter _presenter;

        public override IPresenter GetPresenterByKey(string key)
        {
            if (string.Equals(_key, key))
            {
                return _presenter;
            }

            return null;
        }

        public override string GetPresenterNameByKey(string key)
        {
            if (string.Equals(key, _key))
            {
                return _presenterName;
            }

            return null;
        }

        public override List<PresenterSource> ReadPresenterRegistrators(List<PresenterSource> buffer)
        {
            buffer.Add(new PresenterSource()
            {
                Key = _key,
                Name = _presenterName,
                Source = this
            });
            return buffer;
        }
        
        private void Awake()
        {
            bool bThis = this;
            Debug.Log($"{this.ToString()} - {bThis}", this);
        }
    }
}