using System.Collections.Generic;

namespace Game.CoreLogic
{
    public interface IEcsPresenterConfig
    {
        string GetKey();

        List<IEcsPresenter> GetEcsPresenters();
    }
}