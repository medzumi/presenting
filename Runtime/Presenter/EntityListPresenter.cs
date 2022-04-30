using System;
using ecslite.extensions;
using Game.CoreLogic;
using Unity;
using ViewModel;

namespace Presenter
{
    public sealed class EntityListPresenter<TListComponent> : AbstractEcsPresenter<EntityListPresenter<TListComponent>, TListComponent> 
        where TListComponent : struct, IListComponent<int>
    {
        [PresenterKeyProperty] public string ListElementPresenterKey;
        public string ListPropertyKey;

        private CollectionData _collectionData;
        private readonly Action<int, IViewModel> _action;
        private IEcsPresenter _elementExamplePresenter;

        public EntityListPresenter() : base()
        {
            _action = FillAction;
        }

        public override void Initialize(EcsPresenterData ecsPresenterData)
        {
            base.Initialize(ecsPresenterData);
            _collectionData = ecsPresenterData.ViewModel.GetViewModelData<CollectionData>(ListPropertyKey);
            _elementExamplePresenter = PresenterResolver.Resolve(ListElementPresenterKey);
        }

        protected override void Update(TListComponent data)
        {
            base.Update(data);
            _collectionData.Fill(data.GetList(), _action);
        }

        protected override EntityListPresenter<TListComponent> CloneHandler()
        {
            var clone =  base.CloneHandler();
            clone.ListElementPresenterKey = this.ListElementPresenterKey;
            clone.ListPropertyKey = this.ListPropertyKey;

            return clone;
        }

        protected override void DisposeHandler()
        {
            base.DisposeHandler();
            _elementExamplePresenter.Dispose();
            ListElementPresenterKey = string.Empty;
            ListPropertyKey = string.Empty;
        }

        private void FillAction(int arg1, IViewModel arg2)
        {
            _elementExamplePresenter
                .Clone()
                .Initialize(new EcsPresenterData()
                {
                    ModelWorld = EcsPresenterData.ModelWorld,
                    ModelEntity = arg1,
                    ViewModel = EcsPresenterData.ViewModel
                });
        }
    }
}