using System;
using Animations;
using General.Grid;
using General.Grid.Objects;
using Snake.Parts;
using UnityEngine;
using Zenject;

namespace Snake.Grid
{
    public class SnakeGrid : GameGrid
    {
        [Inject] private SnakeBody m_body;

        
        public bool CanMoveX(int delta)
        {
            if (delta < 0
                    ? m_body.snakeHead.positionOnGrid.x <= 0
                    : m_body.snakeHead.positionOnGrid.x >= m_cells.Length - 1)
            {
                return false;
            }

            var newPosition = m_body.snakeHead.positionOnGrid;
            newPosition.x += delta;

            foreach (var bodySnakePart in m_body.snakeParts)
            {
                if (bodySnakePart.positionOnGrid == newPosition)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CanMoveY(int delta)
        {
            if (delta < 0 
                    ? m_body.snakeHead.positionOnGrid.y <= 0 
                    : m_body.snakeHead.positionOnGrid.y >= m_cells[m_body.snakeHead.positionOnGrid.x].Length - 1)
            {
                return false;
            }

            var newPosition = m_body.snakeHead.positionOnGrid;
            newPosition.y += delta;

            foreach (var bodySnakePart in m_body.snakeParts)
            {
                if (bodySnakePart.positionOnGrid == newPosition)
                {
                    return false;
                }
            }

            return true;
        }

        public ref Cell GetDestinationCell(int deltaX, int deltaY)
        {
            var newPos = m_body.snakeHead.positionOnGrid + new Vector2Int(deltaX, deltaY);
            return ref GetCell(newPos);
        }
    }
}
