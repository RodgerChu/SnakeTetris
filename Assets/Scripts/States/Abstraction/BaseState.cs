using System;
using ResourcesManagement.Abstraction;
using UI;
using Zenject;

namespace States.Abstraction
{
    public abstract class BaseState
    {
        [Inject] protected BaseSceneLoader m_sceneLoader;
        [Inject] protected ViewSystem m_viewManager;

        public virtual void PrepareForActivation(Action<BaseState> onReady)
        {
            onReady(this);
        }
        public abstract void Activate();
        public abstract void Deactivate();
    }
}
