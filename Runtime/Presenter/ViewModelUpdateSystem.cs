using ApplicationScripts.Ecs;
using ApplicationScripts.Ecs.Utility;
using Game.CoreLogic;
using Leopotam.EcsLite;
using Presenting;
using ViewModel;

namespace EcsViewModelPresenting
{
    public class ViewModelUpdateSystem<TComponent> : EcsSystemBase
        where TComponent : struct
    {
        private EcsCollector _ecsCollector;
        private EcsPool<TComponent> _componentPool;
        private EcsPool<DisposableListComponent<IEcsPresenter<TComponent>>> _componentBindDataPool;
        private EcsFilter _filter;

        public override void PreInit(EcsSystems systems)
        {
            var world = systems.GetWorld();
            _ecsCollector = world.Filter<TComponent>().Inc<DisposableListComponent<IEcsPresenter<TComponent>>>()
                .EndCollector(CollectorEvent.Added | CollectorEvent.Dirt);
            _componentPool = world.GetPool<TComponent>();
            _componentBindDataPool = world.GetPool<DisposableListComponent<IEcsPresenter<TComponent>>>();
            _filter = world.Filter<DisposableListComponent<IEcsPresenter<TComponent>>>().Exc<TComponent>().End();
        }

        public override void Run(EcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                _componentBindDataPool.Del(entity);
            }
            
            foreach (var entity in _ecsCollector)
            {
                var componentData = _componentPool.Get(entity);
                foreach (var ecsPresenter in _componentBindDataPool.Get(entity).List)
                {
                    ecsPresenter.Update(componentData);
                }
            }
            _ecsCollector.Clear();
        }
    }
}