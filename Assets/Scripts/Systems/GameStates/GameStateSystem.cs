using System;
using States.Abstraction;

namespace GameCore.States
{
    public class GameStateSystem
    {
        private readonly Action<BaseState> m_stateReadyForActivationHandler;
        private readonly Action<BaseState> m_additionalStateReadyForActivationHandler;

        public GameStateSystem()
        {
            m_stateReadyForActivationHandler = StateReadyForActivationHandler;
            m_additionalStateReadyForActivationHandler = AdditionalStateReadyForActivationHandler;
        }

        private void StateReadyForActivationHandler(BaseState state)
        {
            state.Activate();
            currentState = state;
        }
        
        private void AdditionalStateReadyForActivationHandler(BaseState state)
        {
            state.Activate();
            additionalState = state;
        }

        public BaseState currentState { get; private set; }
        public BaseState additionalState { get; private set; }
        
        public void SwitchTo(BaseState state)
        {
            currentState?.Deactivate();
            state.PrepareForActivation(m_stateReadyForActivationHandler);

        }

        public void ActivateAdditionalState(BaseState state)
        {
            additionalState?.Deactivate();
            state.PrepareForActivation(m_additionalStateReadyForActivationHandler);
        }

        public void DeactivateAdditionalState()
        {
            additionalState?.Deactivate();
            additionalState = null;
        }
    }
}
