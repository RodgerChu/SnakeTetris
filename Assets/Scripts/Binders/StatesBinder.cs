using GameCore.Pooling;
using UI.View;
using UI.View.Collection;
using UnityEngine;
using Zenject;

namespace Binders
{
    public class StatesBinder : MonoInstaller
    {
        public override void InstallBindings()
        {
            var pool = Container.Instantiate<MonoPool<BaseView>>();
            Container.Bind<MonoPool<BaseView>>().FromInstance(pool).AsSingle();
        }
    }
}
