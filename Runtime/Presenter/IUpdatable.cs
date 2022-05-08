using System;
using ApplicationScripts.Ecs;
using ApplicationScripts.Ecs.Utility;
using Leopotam.EcsLite;

namespace Game.CoreLogic
{
    public interface IEcsPresenter<in TModel, in TView> : IDisposable, IPresenter, IClonable<IEcsPresenter<TModel, TView>>
    {
        public void SetPresenterResolver(IPresenterResolver presenterResolver);
        public void SetViewModelResolver(IViewResolver viewResolver);

        public void Initialize(TModel ecsPresenterData, TView tView);
    }

    public interface IDisposeTrigger
    {
        void OnDispose(IDisposable disposable);
    }
    
    public interface IUpdatable<TData>
        where TData : struct
    {
        public void Update(TData? data);
    }

    public interface IComponentProvider
    {
        int CurrentEntity { get; }
        
        int NewEntity();
        
        T ReadReferenceComponent<T>(int entity) where T : class;

        T ReadStructComponent<T>(int entity) where T : struct;

        void SetReferenceComponent<T>(T component, int entity) where T : class;

        void SetStructComponent<T>(T component, int entity) where T : struct;
        
        bool HasComponent<T>(int entity);
    }

    public struct EcsLiteComponentProvider : IComponentProvider
    {
        private readonly EcsWorld _ecsWorld;
        private readonly int _entity;

        public EcsLiteComponentProvider(EcsWorld world, int entity)
        {
            _ecsWorld = world;
            _entity = entity;
        }

        public int CurrentEntity => _entity;

        public int NewEntity()
        {
            return _ecsWorld.NewEntity();
        }

        public T ReadReferenceComponent<T>(int entity) where T : class
        {
            return null;
        }

        public T ReadStructComponent<T>(int entity) where T : struct
        {
            return _ecsWorld.GetPool<T>().Get(entity);
        }

        public void SetReferenceComponent<T>(T component, int entity) where T : class
        {
            
        }

        public void SetStructComponent<T>(T component, int entity) where T : struct
        {
            _ecsWorld.GetPool<T>().EnsureSet(entity, component);
        }

        public bool HasComponent<T>(int entity)
        {
            return _ecsWorld.GetPoolByType(typeof(T)).Has(entity);
        }
    }
}