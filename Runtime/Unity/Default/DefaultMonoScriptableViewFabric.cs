using System.Collections.Generic;
using System.Linq;
using ApplicationScripts.Ecs.Utility;
using Game.CoreLogic;
using UnityEngine;
using unityPresenting.Core;
using Utilities;
using Utilities.CodeExtensions;
using Utilities.Pooling;

namespace unityPresenting.Unity.Default
{
    public class DefaultMonoScriptableViewFabric<TView> : ScriptableViewFabric<TView> where TView : MonoBehaviour
    {
        [SerializeField] private List<TView> _views;

        private readonly Dictionary<string, Pool<TView>> _pools = new Dictionary<string, Pool<TView>>();

        public override object Create(string key)
        {
            if (!_pools.TryGetValue(key, out var pool))
            {
                var exampleView = _views.First(view => string.Equals(view.name, key));
#if UNITY_EDITOR
                var gameObjectName = $"Pool of {typeof(TView).Name}, {key}";
#else
                var gameObjectName = string.Empty;
#endif
                var root = new GameObject(gameObjectName).transform;
                var prebakePoolDisposeHandler = PoolDisposeHandler.Create();
                pool = new Pool<TView>(0, () =>
                {
                    var result = Instantiate(exampleView, root);
                    result.gameObject.SetActive(false);
                    return result;
                }, view =>
                {
                    if (view.TryGetComponent(out IDisposeHandler disposeHandler))
                    {
                        var poolDisposeHandler = prebakePoolDisposeHandler.Clone();
                        poolDisposeHandler.view = view;
                        disposeHandler.Subscribe(poolDisposeHandler);
                    }
                }, view =>
                {
                    if (view)
                    {
                        view.gameObject.SetActive(false);
                        view.transform.SetParent(root);
                    }
                });

                _pools[key] = pool;
                prebakePoolDisposeHandler.pool = pool;
            }

            TView result = pool.Get();
            while (!result)
            {
                result = pool.Get();
            }

            return result;
        }
        
        private class PoolDisposeHandler : PoolableObject<PoolDisposeHandler>, IClonable<PoolDisposeHandler>
        {
            public Pool<TView> pool;
            public TView view;

            protected override void DisposeHandler()
            {
                base.DisposeHandler();
                if (view)
                {
                    pool.Release(view);
                }
            }

            public PoolDisposeHandler Clone()
            {
                var clone = Create();
                clone.pool = pool;
                clone.view = view;
                return clone;
            }
        }

        public override List<ViewData> ReadData(List<ViewData> presenterData)
        {
            if (presenterData.IsNull())
            {
                presenterData = new List<ViewData>();
            }

            foreach (var behaviour in _views)
            {
                presenterData.Add(new ViewData()
                {
                    Key = behaviour.name,
                    view = behaviour
                });
            }

            return presenterData;
        }
    }
    
    
    [CreateAssetMenu(menuName = "presenting/Default/DefaultMonoScriptableViewFabric")]
    public class DefaultMonoScriptableViewFabric : DefaultMonoScriptableViewFabric<MonoBehaviour>
    {
        
    }
}