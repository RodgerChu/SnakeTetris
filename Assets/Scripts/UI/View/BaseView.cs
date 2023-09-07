using System;
using GameCore.Pooling;
using UI.Controllers;
using UI.View.Animation;
using UnityEngine;
using Zenject;

namespace UI.View
{
    [RequireComponent(typeof(ViewAnimationController))]
    public abstract class BaseView : PoolableMonoBehaviour
    {
        protected ViewAnimationController m_viewAnimationController;

        [Inject] protected ViewSystem viewSystem;

        protected virtual void Awake()
        {
            m_viewAnimationController = GetComponent<ViewAnimationController>();
        }

        public void Show()
        {
            m_viewAnimationController.PlayAnimation(ViewAnimation.Show);
            OnShow();
        }

        public void Hide()
        {
            m_viewAnimationController.PlayAnimation(ViewAnimation.Hide);
            OnHide();
        }
        
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }
    }
    
    [RequireComponent(typeof(ViewAnimationController))]
    public abstract class BaseView<T> : BaseView where T: BaseViewController
    {
        protected T m_controller;
        
        public void SetController(T controller)
        {
            m_controller = controller;
        }
    }
}
