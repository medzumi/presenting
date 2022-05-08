using System.Collections.Generic;
using UnityEngine;
using Utilities.SerializeReferencing;
using ViewModel;

namespace Game.CoreLogic
{
    public sealed class AggregatePresenter : AbstractEcsPresenter<AggregatePresenter, IViewModel>
    {
        [SerializeReference] [SerializeTypes(typeof(IEcsPresenter<EcsPresenterData, IViewModel>))]
        private List<IEcsPresenter<EcsPresenterData, IViewModel>> _presenters = new List<IEcsPresenter<EcsPresenterData, IViewModel>>();

        public override void Initialize(EcsPresenterData ecsPresenterData, IViewModel view)
        {
            base.Initialize(ecsPresenterData, view);
            foreach (var ecsPresenter in _presenters)
            {
                ecsPresenter.Initialize(ecsPresenterData, view);
            }
        }

        public AggregatePresenter() : base()
        {
        }

        public AggregatePresenter(List<IEcsPresenter<EcsPresenterData, IViewModel>> presenters) : this()
        {
            _presenters.AddRange(presenters);
        }

        protected override void DisposeHandler()
        {
            base.DisposeHandler();
            foreach (var ecsPresenter in _presenters)
            {
                ecsPresenter.Dispose();
            }
            _presenters.Clear();
        }

        protected override AggregatePresenter CloneHandler()
        {
            var clone = base.CloneHandler();
            foreach (var ecsPresenter in _presenters)
            {
                clone._presenters.Add(ecsPresenter.Clone());
            }

            return clone;
        }
    }
}