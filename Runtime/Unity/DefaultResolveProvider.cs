using System.Collections.Generic;
using System.Linq;
using ApplicationScripts.CodeExtensions;
using Game.CoreLogic;
using UnityEngine;
using ViewModel;

namespace Unity
{
    [CreateAssetMenu]
    public class DefaultResolveProvider : AbstractResolverProvider, IPresenterResolver, IViewModelResolver
    {
        [SerializeField] private DefaultPresenterConfig _defaultPresenterConfig;
        [SerializeField] private List<MonoViewModel> _monoViewModels;

        private readonly Dictionary<string, IEcsPresenter> _ecsPresenters = new Dictionary<string, IEcsPresenter>();

        private readonly List<IEcsPresenter> _buffer = new List<IEcsPresenter>();

        public override IPresenterResolver ProvidePresenterResolver()
        {
            return this;
        }

        public override IViewModelResolver ProvideViewModelResolver()
        {
            return this;
        }

        public List<string> GetViewModelKeys(List<string> buffer = null)
        {
            if (buffer.IsNull())
            {
                buffer = new List<string>();
            }

            buffer.AddRange(_monoViewModels.Select(model => model.name));
            return buffer;
        }

        IViewModel IViewModelResolver.Resolve(string key)
        {
            return _monoViewModels.FirstOrDefault(model => string.Equals(model.name, key));
        }

        IEcsPresenter IPresenterResolver.Resolve(string key)
        {
            if (!_ecsPresenters.TryGetValue(key, out var presenter))
            {
                var config = _defaultPresenterConfig.GetEcsPresenterConfigs()
                    .FirstOrDefault(config => string.Equals(key, config.GetKey()));
            
                _buffer.Clear();
                if (config != null)
                {
                    foreach (var ecsPresenter in config.GetEcsPresenters())
                    {
                        _buffer.Add(ecsPresenter.Clone());
                    }
                }

                _ecsPresenters[key] = presenter = new AggregatePresenter(_buffer);
            }

            return presenter.Clone();
        }
    }
}