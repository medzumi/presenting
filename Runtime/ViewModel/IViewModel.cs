using System;
using System.Threading;
using UnityEngine;

namespace ViewModel
{
    public interface IViewModel
    {
        T GetViewModelData<T>(string propertyName) where T : IViewModelData;

        T AddTo<T>(T disposable) where T : IDisposable;
    }
}