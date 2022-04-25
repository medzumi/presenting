using System;
using Game.CoreLogic;
using UniRx;
using ViewModel;

namespace Presenter
{
    public class OpenPresenter : AbstractEcsPresenter<OpenPresenter>
    {
        public string OpenCommandKey;
        public string PresenterKey;

        private IViewModelEvent<NullData> NullEvent;
        private Action<NullData> _action;

        public OpenPresenter() : base()
        {
            _action = OpenPresenterMethod;
        }

        private void OpenPresenterMethod(NullData obj)
        {
            var presenter = Resolve(PresenterKey, out var viewModel);
            presenter.Initialize(new EcsPresenterData()
            {
                ModelEntity = EcsPresenterData.ModelEntity,
                ModelWorld = EcsPresenterData.ModelWorld,
                ViewModel = viewModel
            });
        }

        public override void Initialize(EcsPresenterData ecsPresenterData)
        {
            base.Initialize(ecsPresenterData);
            NullEvent = ecsPresenterData.ViewModel.GetViewModelData<IViewModelEvent<NullData>>(OpenCommandKey);
            var disposable = NullEvent.Subscribe(_action);
            AddTo(disposable);
            EcsPresenterData.ViewModel.AddTo(disposable);
        }
    }
}