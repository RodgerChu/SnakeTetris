using GameCore.Pooling;
using GameCore.States;
using States.Abstraction;
using UI;
using Zenject;

namespace Binders
{
    public class GameCoreBinder : MonoInstaller
    {
        public override void InstallBindings()
        {
            var viewSystem = Container.Instantiate<ViewSystem>();
            Container.Bind<ViewSystem>().FromInstance(viewSystem).AsSingle();

            var pool = Container.Instantiate<Pool<BaseState>>();
            Container.Bind<Pool<BaseState>>().FromInstance(pool).AsSingle();

            var statesSystem = Container.Instantiate<GameStateSystem>();
            Container.Bind<GameStateSystem>().FromInstance(statesSystem).AsSingle();
        }
    }
}