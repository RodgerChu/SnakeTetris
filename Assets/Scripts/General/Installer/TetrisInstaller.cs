using Tetris.Grid;
using UnityEngine;
using Zenject;

namespace General.Installer
{
    public class TetrisInstaller : MonoInstaller
    {
        [SerializeField] private TetrisGrid m_tetrisGrid;
        
        public override void InstallBindings()
        {
            Container.Bind<TetrisGrid>().FromInstance(m_tetrisGrid).AsSingle();
        }
    }
}
