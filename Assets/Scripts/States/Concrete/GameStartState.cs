using System;
using GameCore.Debug;
using States.Abstraction;
using Zenject;

namespace GameCore.States.Concrete
{
    public class GameStartState: BaseState
    {
        [Inject] private ConsoleLogger m_logger;
        
        public override void PrepareForActivation(Action<BaseState> onReady)
        {
            
            
            base.PrepareForActivation(onReady);
        }

        public override void Activate()
        {
            m_logger.Log("WOHO");
        }

        public override void Deactivate()
        {
            
        }
    }
}