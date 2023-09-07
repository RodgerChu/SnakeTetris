using ResourcesManagement.Abstraction;
using ResourcesManagement.Concrete;
using UI.View.Collection;
using UnityEngine;
using Zenject;

namespace Binders
{
    public class UIBinder : MonoInstaller
    {
        [SerializeField] private Canvas m_canvas;
        [SerializeField] private ViewCollection m_viewCollection;
        
        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromInstance(m_canvas).AsSingle();
            Container.Bind<ViewCollection>().FromInstance(m_viewCollection).AsSingle();

            var scenesLoader = Container.Instantiate<UnityResourcesSceneLoader>();
            Container.Bind<BaseSceneLoader>().FromInstance(scenesLoader).AsSingle();
        }
    }
}
