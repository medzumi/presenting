using System;
using ApplicationScripts.Ecs.Utility;
using Utilities;

namespace unityPresenting.Core
{
    public interface IPresenter : IDisposable
    {
        Type GetModelType();
        Type GetViewType();
        
        void Initialize(object model, object view);
    }
    
    public interface IPresenter<in TModel, in TView> : IPresenter, IClonable<IPresenter<TModel, TView>>
    {
        public void Initialize(TModel ecsPresenterData, TView tView);
    }
}