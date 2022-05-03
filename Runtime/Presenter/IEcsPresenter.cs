using System;
using ApplicationScripts.Ecs.Utility;

namespace Game.CoreLogic
{
    public interface IEcsPresenter : IDisposable, IClonable<IEcsPresenter>
    {
        public void SetPresenterResolver(IPresenterResolver presenterResolver);
        public void SetViewModelResolver(IViewModelResolver viewModelResolver);
        
        public void Initialize(EcsPresenterData ecsPresenterData);
    }
    
    public interface IEcsPresenter<TData> : IDisposable, IEcsPresenter
        where TData : struct
    {
        public void Update(TData? data);
    }
}