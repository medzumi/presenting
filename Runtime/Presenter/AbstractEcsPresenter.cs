using System;
using ApplicationScripts.Ecs;
using Leopotam.EcsLite;
using Utilities.GenericPatterns;
using ViewModel;

namespace Game.CoreLogic
{
    [Serializable]
    public abstract class AbstractEcsPresenter<TPresenter> : PoolableObject<TPresenter>, IEcsPresenter
        where TPresenter : AbstractEcsPresenter<TPresenter>, new()
    {
        private EcsPresenterData _ecsPresenterData;

        protected EcsPresenterData EcsPresenterData
        {
            get => _ecsPresenterData;
            private set => _ecsPresenterData = value;
        }
        protected EcsPool<DisposableListComponent<IDisposable>> DisposeComponentPool { get; private set; }

        protected IEcsPresenter Resolve(string key)
        {
            return Singletone<IPresenterResolver>.instance.Resolve(key);
        }

        protected IEcsPresenter Resolve(string key, out IViewModel viewModel)
        {
            return Singletone<IPresenterResolver>.instance.Resolve(key, out viewModel);
        }

        public virtual void Initialize(EcsPresenterData ecsPresenterData)
        {
            EcsPresenterData = ecsPresenterData;
            DisposeComponentPool = ecsPresenterData.ModelWorld.GetPool<DisposableListComponent<IDisposable>>();
            DisposeComponentPool.EnsureGet(ecsPresenterData.ModelEntity).List.Add(this);
            ecsPresenterData.ViewModel.AddTo(this);
        }
        
        
        public IEcsPresenter Clone()
        {
            return CloneHandler();
        }

        protected virtual TPresenter CloneHandler()
        {
            return AbstractEcsPresenter<TPresenter>.Create();
        }
    }
    
    [Serializable]
    public abstract class AbstractEcsPresenter<TPresenter, TData> : AbstractEcsPresenter<TPresenter>, IEcsPresenter<TData>
        where TPresenter : AbstractEcsPresenter<TPresenter, TData>, new()
    {
        protected EcsPool<DisposableListComponent<IEcsPresenter<TData>>> _presentersPool;

        public override void Initialize(EcsPresenterData ecsPresenterData)
        {
            base.Initialize(ecsPresenterData);
            _presentersPool = ecsPresenterData.ModelWorld.GetPool<DisposableListComponent<IEcsPresenter<TData>>>();
            _presentersPool.EnsureGet(ecsPresenterData.ModelEntity).List.Add(this);
        }

        public virtual void Update(TData data)
        {
        }

        protected override void DisposeHandler()
        {
            base.DisposeHandler();
            var data = _presentersPool.Get(EcsPresenterData.ModelEntity).List;
            data.Remove(this);
            if (data.Count == 0)
            {
                _presentersPool.Del(EcsPresenterData.ModelEntity);
            }
        }
    }
}