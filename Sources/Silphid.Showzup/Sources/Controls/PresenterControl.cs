﻿using System;
using log4net;
using Silphid.Extensions;
using UniRx;
using UnityEngine;

namespace Silphid.Showzup
{
    public abstract class PresenterControl : Control, IPresenter
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PresenterControl));
        
        #region Private

        private Transform _instantiationContainer;

        private void Awake()
        {
            if (Log.IsDebugEnabled)
                MutableState.Subscribe(x => Log.Debug($"State: {x}\r\nPresenter: {gameObject.ToHierarchyPath()}")).AddTo(this);
        }

        #endregion
        
        #region Protected

        protected IReactiveProperty<PresenterState> MutableState { get; } = new ReactiveProperty<PresenterState>();
        protected IReactiveProperty<IView> MutableFirstView = new ReactiveProperty<IView>((IView) null);

        protected Transform GetInstantiationContainer()
        {
            if (_instantiationContainer == null)
            {
                var obj = new GameObject("InstantiationContainer");
                obj.SetActive(false);
                _instantiationContainer = obj.transform;
                _instantiationContainer.transform.parent = transform;
            }

            return _instantiationContainer;
        }

        #endregion
        
        #region Public

        public IReadOnlyReactiveProperty<IView> FirstView => MutableFirstView;

        #endregion

        #region IPresenter members

        public abstract IObservable<IView> Present(object input, Options options = null);
        public virtual IReadOnlyReactiveProperty<PresenterState> State => MutableState;
        
        #endregion
    }
}