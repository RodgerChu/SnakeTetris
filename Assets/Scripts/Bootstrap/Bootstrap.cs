using GameCore.Pooling;
using GameCore.States;
using GameCore.States.Concrete;
using States.Abstraction;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [Inject] private GameStateSystem m_stateSystem;
        [Inject] private Pool<BaseState> m_statesPool;

        void Start()
        {
            m_stateSystem.SwitchTo(m_statesPool.Get<BootstrapState>());
        }
    }
}
