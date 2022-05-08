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
    
    public class EntityListPresenter<TPresenter, TListComponent> : AbstractEcsPresenter<TPresenter, IViewModel, TListComponent> 
        where TListComponent : struct, IListComponent<int>
        where TPresenter : EntityListPresenter<TPresenter, TListComponent>, new()
    {
        [PresenterKeyProperty(typeof(EcsPresenterData), typeof(IViewModel))] public string ListElementPresenterKey;
        [PresenterKeyProperty(typeof(EcsPresenterData), typeof(IViewModel))] public string RootToListElementPresenterKey;
        [ViewKeyProperty(typeof(IViewModel))] public string ListElementViewModelKey;
        public string ListPropertyKey;

        private CollectionData _collectionData;
        private readonly Func<int, IViewModel> _action;
        private IEcsPresenter<EcsPresenterData, IViewModel> _elementExamplePresenter;
        private IEcsPresenter<EcsPresenterData, IViewModel> _rootElementExamplePresenter;
        private IConcreteResolver<IViewModel> _concreteResolver;

        public EntityListPresenter() : base()
        {
            _action = FillAction;
        }

        public override void Initialize(EcsPresenterData ecsPresenterData, IViewModel viewModel)
        {
            base.Initialize(ecsPresenterData, viewModel);
            _collectionData = viewModel.GetViewModelData<CollectionData>(ListPropertyKey);
            _elementExamplePresenter = PresenterResolver.Resolve<EcsPresenterData, IViewModel>(ListElementPresenterKey);
            _rootElementExamplePresenter = PresenterResolver.Resolve<EcsPresenterData, IViewModel>(RootToListElementPresenterKey);
            _concreteResolver = ViewResolver.GetResolver<IViewModel>(ListElementViewModelKey);
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
            clone.RootToListElementPresenterKey = this.RootToListElementPresenterKey;
            clone.ListElementViewModelKey = this.ListElementViewModelKey;
            return clone;
        }

        protected override void DisposeHandler()
        {
            base.DisposeHandler();
            _elementExamplePresenter.Dispose();
            ListElementPresenterKey = string.Empty;
            ListPropertyKey = string.Empty;
        }

        [Obsolete("Temporary method. Better don't use")]
        protected virtual IViewModel ResolveElementViewModel(int arg)
        {
            return _concreteResolver.Resolve();
        }

        private IViewModel FillAction(int arg1)
        {
            var viewModel = ResolveElementViewModel(arg1);
            _elementExamplePresenter
                .Clone()
                .Initialize(new EcsPresenterData()
                {
                    ModelWorld = EcsPresenterData.ModelWorld,
                    ModelEntity = arg1,
                }, viewModel);

            _rootElementExamplePresenter
                .Clone()
                .Initialize(new EcsPresenterData()
                {
                    ModelWorld = EcsPresenterData.ModelWorld,
                    ModelEntity = EcsPresenterData.ModelEntity,
                }, viewModel);
            
            return viewModel;
        }
    }
}