using GameCore.Debug;
using Zenject;

namespace Binders
{
    public class DebugBinder : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ConsoleLogger>().FromInstance(new ConsoleLogger()).AsSingle();
        }
    }
}
