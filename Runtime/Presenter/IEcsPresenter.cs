using System;
using ApplicationScripts.Ecs.Utility;

namespace Game.CoreLogic
{
    public interface IEcsPresenter : IDisposable, IClonable<IEcsPresenter>
    {
        public void Initialize(EcsPresenterData ecsPresenterData);
    }
    
    public interface IEcsPresenter<TData> : IDisposable, IEcsPresenter
    {
        public void Update(TData data);
    }
}