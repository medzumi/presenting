using System;
using Game.CoreLogic;
using UniRx;
using Unity;
using ViewModel;

namespace Presenter
{
    public class OpenPresenter : AbstractEcsPresenter<OpenPresenter>
    {
        public string OpenCommandKey;
        [PresenterKeyProperty] public string PresenterKey;
        [MonoViewModelKeyProperty] public string ViewModelKey;

        private IViewModelEvent<NullData> NullEvent;
        private Action<NullData> _action;

        public OpenPresenter() : base()
        {
            _action = OpenPresenterMethod;
        }

        private void OpenPresenterMethod(NullData obj)
        {
            var presenter = PresenterResolver.Resolve(PresenterKey);
            var viewModel = ViewModelResolver.Resolve(ViewModelKey);
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