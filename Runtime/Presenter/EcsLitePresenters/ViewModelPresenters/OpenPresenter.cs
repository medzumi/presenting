using System;
using Game.CoreLogic;
using UniRx;
using Unity;
using ViewModel;

namespace Presenter
{
    public class OpenPresenter : AbstractEcsPresenter<OpenPresenter, IViewModel>
    {
        public string OpenCommandKey;
        [PresenterKeyProperty(typeof(EcsPresenterData), typeof(IViewModel))] public string PresenterKey;
        [ViewKeyProperty(typeof(IViewModel))] public string ViewModelKey;

        private IViewModelEvent<NullData> NullEvent;
        private Action<NullData> _action;

        public OpenPresenter() : base()
        {
            _action = OpenPresenterMethod;
        }

        private void OpenPresenterMethod(NullData obj)
        {
            var presenter = PresenterResolver.Resolve<EcsPresenterData, IViewModel>(PresenterKey);
            var viewModel = ViewResolver.Resolve<IViewModel>(ViewModelKey);
            presenter.Initialize(new EcsPresenterData()
            {
                ModelEntity = EcsPresenterData.ModelEntity,
                ModelWorld = EcsPresenterData.ModelWorld,
            }, viewModel);
        }

        public override void Initialize(EcsPresenterData ecsPresenterData, IViewModel viewModel)
        {
            base.Initialize(ecsPresenterData, viewModel);
            NullEvent = viewModel.GetViewModelData<IViewModelEvent<NullData>>(OpenCommandKey);
            var disposable = NullEvent.Subscribe(_action);
            AddTo(disposable);
            viewModel.AddTo(disposable);
        }
    }
}