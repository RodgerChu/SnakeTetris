using Snake.Collectables;
using Snake.Grid;
using Snake.Parts;
using UnityEngine;
using Zenject;

namespace Snake.Movement
{
    public class SnakeMovement: MonoBehaviour
    {
        [Inject] private SnakeGrid m_grid;
        [Inject] private IMovementInput m_movementInput;
        [Inject] private SnakeBodyPartsCollectables m_collectables; 

        private float m_cooldown;
        
        private Vector2Int m_snakeHeadPosition;
        
        public void Update()
        {
            if (m_cooldown > 0)
            {
                m_cooldown -= Time.deltaTime;
                return;
            }
            
            var input = m_movementInput.GetInput();
            if (input.x != 0 && m_grid.CanMoveX(input.x))
            {
                m_collectables.OnBeforeSnakeMovement(ref m_grid.GetDestinationCell(input.x, 0));
                m_grid.MoveSnakeX(input.x);
                m_collectables.OnAfterSnakeMovement();
                m_cooldown = .3f;
            }
            else if (input.y != 0 && m_grid.CanMoveY(input.y))
            {
                m_collectables.OnBeforeSnakeMovement(ref m_grid.GetDestinationCell(0, input.y));
                m_grid.MoveSnakeY(input.y);
                m_collectables.OnAfterSnakeMovement();
                m_cooldown = .3f;
            }
        }
    }
}
