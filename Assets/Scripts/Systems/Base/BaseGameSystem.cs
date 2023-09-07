using Zenject;

namespace GameCore.Systems.Base
{
    public abstract class BaseGameSystem
    {
        [Inject] private GameTickSystem m_tickSystem;
        
        public BaseGameSystem()
        {
            m_tickSystem.AddSystem(this);
        }
        
        public virtual void OnSystemsEarlyUpdate() { }
        public virtual void OnSystemsLateUpdate() { }
        public virtual void OnFrameEnd() { }
    }
}