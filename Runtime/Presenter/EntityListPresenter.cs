using System;
using ecslite.extensions;
using Game.CoreLogic;
using Unity;
using ViewModel;

namespace Presenter
{
    public sealed class EntityListPresenter<TListComponent> : EntityListPresenter<EntityListPresenter<TListComponent>, TListComponent> 
        where TListComponent : struct, IListComponent<int>
    {
    }
    
    public class EntityListPresenter<TPresenter, TListComponent> : AbstractEcsPresenter<TPresenter, TListComponent> 
        where TListComponent : struct, IListComponent<int>
        where TPresenter : EntityListPresenter<TPresenter, TListComponent>, new()
    {
        [PresenterKeyProperty] public string ListElementPresenterKey;
        [MonoViewModelKeyProperty] public string ListElementViewModelKey;
        public string ListPropertyKey;

        private CollectionData _collectionData;
        private readonly Func<int, IViewModel> _action;
        private IEcsPresenter _elementExamplePresenter;
        private IConcreteResolver _concreteResolver;

        public EntityListPresenter() : base()
        {
            _action = FillAction;
        }

        public override void Initialize(EcsPresenterData ecsPresenterData)
        {
            base.Initialize(ecsPresenterData);
            _collectionData = ecsPresenterData.ViewModel.GetViewModelData<CollectionData>(ListPropertyKey);
            _elementExamplePresenter = PresenterResolver.Resolve(ListElementPresenterKey);
            _concreteResolver = ViewModelResolver.GetResolver(ListElementViewModelKey);
        }

        protected override void Update(TListComponent data)
        {
            base.Update(data);
            _collectionData.Fill(data.GetList(), _action);
        }

        protected override TPresenter CloneHandler()
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

        private IViewModel FillAction(int arg1)
        {
            var viewModel = _concreteResolver.Resolve();
            _elementExamplePresenter
                .Clone()
                .Initialize(new EcsPresenterData()
                {
                    ModelWorld = EcsPresenterData.ModelWorld,
                    ModelEntity = arg1,
                    ViewModel = viewModel
                });

            return viewModel;
        }
    }
}