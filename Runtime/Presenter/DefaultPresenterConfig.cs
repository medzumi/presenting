using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationScripts.CodeExtensions;
using Presenter;
using UnityEngine;
using ViewModel;

namespace Game.CoreLogic
{
    [CreateAssetMenu]
    [Serializable]
    public class DefaultPresenterConfig : ScriptablePresenterCollection
    {
        [SerializeField] private List<PresenterConfig> _presenter;
        public override List<PresenterData> ReadPresenterData(List<PresenterData> presenterData)
        {
            if (presenterData.IsNull())
            {
                presenterData = new List<PresenterData>();
            }

            foreach (var presenterConfig in _presenter)
            {
                var presenter = presenterConfig.GetPresenter();
                if (presenter != null)
                {
                    var presenterInterfaces = presenter.GetType().GetInterfaces();
                    foreach (var type in presenterInterfaces)
                    {
                        if (type.IsGenericType)
                        {
                            var genericDefinition = type.GetGenericTypeDefinition();
                            if (genericDefinition != null && genericDefinition == typeof(IEcsPresenter<,>))
                            {
                                var arguments = type.GetGenericArguments();
                                presenterData.Add(new PresenterData()
                                {
                                    ModelType = arguments[0],
                                    ViewType = arguments[1],
                                    Key = presenterConfig.GetKey(),
                                    Presenter = presenter
                                });
                            }
                        }
                    }
                }
            }

            return presenterData;
        }
    }
}