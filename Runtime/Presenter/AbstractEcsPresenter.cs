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
        public IViewModelResolver ViewModelResolver;
        public IPresenterResolver PresenterResolver;
         
        private EcsPresenterData _ecsPresenterData;

        protected EcsPresenterData EcsPresenterData
        {
            get => _ecsPresenterData;
            private set => _ecsPresenterData = value;
        }
        protected EcsPool<DisposableListComponent<IDisposable>> DisposeComponentPool { get; private set; }

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
            var clone = AbstractEcsPresenter<TPresenter>.Create();
            clone.PresenterResolver = PresenterResolver;
            clone.ViewModelResolver = ViewModelResolver;
            return clone;
        }
    }
    
    [Serializable]
    public abstract class AbstractEcsPresenter<TPresenter, TData> : AbstractEcsPresenter<TPresenter>, IEcsPresenter<TData>
        where TPresenter : AbstractEcsPresenter<TPresenter, TData>, new()
        where TData : struct
    {
        public string HasComponentKey;
        
        protected EcsPool<DisposableListComponent<IEcsPresenter<TData>>> _presentersPool;
        private IViewModelProperty<bool> _hasComponentProperty;

        public override void Initialize(EcsPresenterData ecsPresenterData)
        {
            base.Initialize(ecsPresenterData);
            _presentersPool = ecsPresenterData.ModelWorld.GetPool<DisposableListComponent<IEcsPresenter<TData>>>();
            _presentersPool.EnsureGet(ecsPresenterData.ModelEntity).List.Add(this);
            _hasComponentProperty =
                ecsPresenterData.ViewModel.GetViewModelData<IViewModelProperty<bool>>(HasComponentKey);
        }

        public void Update(TData? data)
        {
            if (data.HasValue)
            {
                if (_hasComponentProperty?.GetValue() == false)
                {
                    _hasComponentProperty.SetValue(true);
                }
                
                Update(data.Value);
            }
            else
            {
                if (_hasComponentProperty?.GetValue() == true)
                {
                    _hasComponentProperty.SetValue(false);
                }
            }
        }

        protected virtual void Update(TData data)
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