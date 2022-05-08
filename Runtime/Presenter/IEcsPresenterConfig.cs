using System.Collections.Generic;

namespace Game.CoreLogic
{
    public interface IEcsPresenterConfig<TModel, TView>
    {
        string GetKey();

        IEcsPresenter<TModel, TView> GetPresenter();
    }
}