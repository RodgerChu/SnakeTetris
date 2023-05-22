using Snake.Collectables;
using Snake.Grid;
using Snake.Movement;
using Snake.Parts;
using UnityEngine;
using Zenject;

namespace General.Installer
{
    public class SnakeInstaller : MonoInstaller
    {
        [SerializeField] private SnakeBody m_body;
        [SerializeField] private SnakeGrid m_grid;
        [SerializeField] private SnakeMovement m_movement;
        [SerializeField] private SnakeBodyPartsCollectables m_collectableSnakePart;
        
        public override void InstallBindings()
        {
            Container.Bind<SnakeBody>().FromInstance(m_body).AsSingle();
            Container.Bind<SnakeGrid>().FromInstance(m_grid).AsSingle();

            Container.Bind<SnakeBodyPartsCollectables>().FromInstance(m_collectableSnakePart).AsSingle();
            Container.Bind<SnakeMovement>().FromInstance(m_movement).AsSingle();
        }
    }
}
