using UnityEngine;
using Zenject;

namespace General.Installer
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private SimpleKeyboardInput m_input;
        
        public override void InstallBindings()
        {
            Container.Bind<IMovementInput>().FromInstance(m_input).AsSingle();
        }
    }
}
