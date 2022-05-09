using System;
using ApplicationScripts.Ecs.Utility;
using Utilities;

namespace unityPresenting.Core
{
    public interface IPresenter
    {
        Type GetModelType();
        Type GetViewType();
        
        void Initialize(object model, object view);
    }
    
    public interface IPresenter<in TModel, in TView> : IDisposable, IPresenter, IClonable<IPresenter<TModel, TView>>
    {
        public void Initialize(TModel ecsPresenterData, TView tView);
    }
}