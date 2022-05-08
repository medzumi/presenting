using System;
using ApplicationScripts.Ecs;
using ApplicationScripts.Ecs.Utility;
using Leopotam.EcsLite;
using Utilities.GenericPatterns;
using ViewModel;

namespace Game.CoreLogic
{
    [Serializable]
    public abstract class AbstractEcsPresenter<TPresenter, TView> : PoolableObject<TPresenter>, IEcsPresenter<EcsPresenterData, TView>
        where TPresenter : AbstractEcsPresenter<TPresenter, TView>, new()
    {
        public IViewResolver ViewResolver;
        public IPresenterResolver PresenterResolver;
         
        private EcsPresenterData _ecsPresenterData;
        private TView _view;

        protected EcsPresenterData EcsPresenterData
        {
            get => _ecsPresenterData;
            private set => _ecsPresenterData = value;
        }

        protected TView View => _view;
        
        protected EcsPool<DisposableListComponent> DisposeComponentPool { get; private set; }

        public void SetPresenterResolver(IPresenterResolver presenterResolver)
        {
            PresenterResolver = presenterResolver;
        }

        public void SetViewModelResolver(IViewResolver viewResolver)
        {
            ViewResolver = viewResolver;
        }

        public virtual void Initialize(EcsPresenterData ecsPresenterData, TView view)
        {
            _view = view;
            EcsPresenterData = ecsPresenterData;
            DisposeComponentPool = ecsPresenterData.ModelWorld.GetPool<DisposableListComponent>();
            DisposeComponentPool.EnsureGet(ecsPresenterData.ModelEntity).List.Add(this);
        }
        
        
        public IEcsPresenter<EcsPresenterData, TView> Clone()
        {
            return CloneHandler();
        }

        protected virtual TPresenter CloneHandler()
        {
            var clone = AbstractEcsPresenter<TPresenter, TView>.Create();
            clone.PresenterResolver = PresenterResolver;
            clone.ViewResolver = ViewResolver;
            return clone;
        }
    }
    
    [Serializable]
    public abstract class AbstractEcsPresenter<TPresenter, TView, TData> : AbstractEcsPresenter<TPresenter, TView>, IUpdatable<TData>
        where TPresenter : AbstractEcsPresenter<TPresenter, TView, TData>, new()
        where TData : struct
    {
        public string HasComponentKey;
        
        protected EcsPool<ListComponent<IUpdatable<TData>>> _updatablePool;

        public override void Initialize(EcsPresenterData ecsPresenterData, TView view)
        {
            base.Initialize(ecsPresenterData, view);
            _updatablePool = ecsPresenterData.ModelWorld.GetPool<ListComponent<IUpdatable<TData>>>();
            _updatablePool.EnsureGet(ecsPresenterData.ModelEntity).List.Add(this);
        }

        public void Update(TData? data)
        {
            if (data.HasValue)
            {
                Update(data.Value);
            }
        }

        protected virtual void Update(TData data)
        {
            
        }

        protected override void DisposeHandler()
        {
            base.DisposeHandler();
            var data = _updatablePool.Get(EcsPresenterData.ModelEntity).List;
            data.Remove(this);
            if (data.Count == 0)
            {
                _updatablePool.Del(EcsPresenterData.ModelEntity);
            }
        }
    }
}