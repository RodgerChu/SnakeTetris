using Zenject;

namespace Binders
{
    public class MovementInputBinder : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SimpleKeyboardInput>().FromInstance(new SimpleKeyboardInput()).AsSingle();
        }
    }
}