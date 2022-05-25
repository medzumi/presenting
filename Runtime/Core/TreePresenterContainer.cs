using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using unityPresenting.Unity;
using Utilities.CodeExtensions;
using Utilities.Unity.Buttons;
using Utilities.Unity.Extensions;
using Utilities.Unity.SerializeReferencing;

namespace unityPresenting.Core
{
    [CreateAssetMenu]
    public class TreePresenterContainer : ConcretePresenterContainer
    {
        [Serializable]
        private class TreeData
        {
            public string Name;
            [GuidStringKey] public string _key = Guid.NewGuid().ToString();

            [SerializeReference] [SerializeTypes(typeof(IPresenter))]
            public IPresenter _Presenter = null;
        }

        [SerializeField] private List<ConcretePresenterContainer> _concretePresenterContainers;

        public override List<PresenterSource> ReadPresenterRegistrators(List<PresenterSource> buffer)
        {
            foreach (var concretePresenterContainer in _concretePresenterContainers)
            {
                concretePresenterContainer.ReadPresenterRegistrators(buffer);
            }
            return base.ReadPresenterRegistrators(buffer);
        }

        public override IPresenter GetPresenterByKey(string key)
        {
            var result = base.GetPresenterByKey(key);
            if (result.IsNullInUnity())
            {
                foreach (var concretePresenterContainer in _concretePresenterContainers)
                {
                    result = concretePresenterContainer.GetPresenterByKey(key);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        public override string GetPresenterNameByKey(string key)
        {
            var result = base.GetPresenterNameByKey(key);
            if (result.IsNull())
            {
                foreach (var concretePresenterContainer in _concretePresenterContainers)
                {
                    result = concretePresenterContainer.GetPresenterNameByKey(key);
                    if (result != null)
                        return result;
                }
            }

            return result;
        }

        [Button(Name = "Test")]
        private void Test()
        {
            var concrete = new ConcretePresenterContainer();
            AssetDatabase.AddObjectToAsset(concrete, AssetDatabase.GetAssetPath(this));
            _concretePresenterContainers.Add(concrete);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
}