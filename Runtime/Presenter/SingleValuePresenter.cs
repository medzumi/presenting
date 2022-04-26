using Game.CoreLogic;
using Packages.ecslite.extensions.CommonComponents;
using ViewModel;

namespace Presenter
{
    public class SingleValuePresenter<TDataComponent, TValue> : AbstractEcsPresenter<SingleValuePresenter<TDataComponent, TValue>, TDataComponent>
        where TDataComponent : struct, ISingleValueCompnent<TValue>
    {
        public string ValueKey;

        private IViewModelProperty<TValue> _valueProperty;

        public override void Initialize(EcsPresenterData ecsPresenterData)
        {
            base.Initialize(ecsPresenterData);
            _valueProperty = ecsPresenterData.ViewModel.GetViewModelData<IViewModelProperty<TValue>>(ValueKey);
        }

        protected override void Update(TDataComponent data)
        {
            base.Update(data);
            _valueProperty.SetValue(data.GetValue());
        }
    }
}