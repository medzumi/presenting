using System;
using ecslite.extensions;
using Game.CoreLogic;
using ViewModel;

namespace Presenter
{
    public sealed class EntityListPresenter<TListComponent> : AbstractEcsPresenter<EntityListPresenter<TListComponent>, TListComponent> 
        where TListComponent : struct, IListComponent<int>
    {
        public string ListElementPresenterKey;
        public string ListPropertyKey;

        private CollectionData _collectionData;
        private readonly Action<int, IViewModel> _action;

        public EntityListPresenter() : base()
        {
            _action = FillAction;
        }

        public override void Initialize(EcsPresenterData ecsPresenterData)
        {
            base.Initialize(ecsPresenterData);
            _collectionData = ecsPresenterData.ViewModel.GetViewModelData<CollectionData>(ListPropertyKey);
        }

        protected override void Update(TListComponent data)
        {
            base.Update(data);
            _collectionData.Fill(data.GetList(), _action);
        }

        private void FillAction(int arg1, IViewModel arg2)
        {
            ResolvePresenter(ListElementPresenterKey)
                .Initialize(new EcsPresenterData()
                {
                    ModelWorld = EcsPresenterData.ModelWorld,
                    ModelEntity = arg1,
                    ViewModel = EcsPresenterData.ViewModel
                });
        }
    }
}