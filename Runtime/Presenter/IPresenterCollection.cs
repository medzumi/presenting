using System;
using System.Collections.Generic;
using Game.CoreLogic;
using UnityEngine;

namespace Presenter
{
    public interface IPresenterCollection
    {
        List<PresenterData> ReadPresenterData(List<PresenterData> presenterData);
    }

    public abstract class ScriptablePresenterCollection : ScriptableObject, IPresenterCollection
    {
        public abstract List<PresenterData> ReadPresenterData(List<PresenterData> presenterData);
    }

    public struct PresenterData
    {
        public Type ModelType;
        public Type ViewType;
        public string Key;
        public IPresenter Presenter;
    }
}