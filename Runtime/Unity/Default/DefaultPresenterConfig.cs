using System;
using System.Collections.Generic;
using UnityEngine;
using unityPresenting.Core;
using Utilities.CodeExtensions;

namespace unityPresenting.Unity.Default
{
    [CreateAssetMenu(menuName = "unityPresenting/Default/DefaultPresenterConfig")]
    [Serializable]
    public class DefaultPresenterConfig : ScriptablePresenterReadableList
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
                            if (genericDefinition != null && genericDefinition == typeof(IPresenter<,>))
                            {
                                var arguments = type.GetGenericArguments();
                                presenterData.Add(new PresenterData()
                                {
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