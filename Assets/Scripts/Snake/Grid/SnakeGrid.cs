using System;
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

        public void MoveSnakeX(int x)
        {
            var head = m_body.snakeHead;
            
            if (m_body.snakeParts.Count > 0)
            {
                for (int i = m_body.snakeParts.Count - 1; i >= 1; i--)
                {
                    var part = m_body.snakeParts[i];
                    var nextPart = m_body.snakeParts[i - 1];
                    MovePart(part, nextPart, i == m_body.snakeParts.Count - 1);
                }
                MovePart(m_body.snakeParts[0], head, false);
            }

            head.positionOnGrid.x += Math.Sign(x);
            
            ref var nextCell = ref m_cells[head.positionOnGrid.x][head.positionOnGrid.y];
            head.transform.position = nextCell.position;
            nextCell.objectOnCell = head;
        }

        public void MoveSnakeY(int y)
        {
            var head = m_body.snakeHead;
            
            if (m_body.snakeParts.Count > 0)
            {
                for (int i = m_body.snakeParts.Count - 1; i >= 1; i--)
                {
                    var part = m_body.snakeParts[i];
                    var nextPart = m_body.snakeParts[i - 1];
                    MovePart(part, nextPart, i == m_body.snakeParts.Count - 1);
                }
                MovePart(m_body.snakeParts[0], head, false);
            }
            
            head.positionOnGrid.y += Math.Sign(y);
            
            ref var nextCell = ref m_cells[head.positionOnGrid.x][head.positionOnGrid.y];
            head.transform.position = nextCell.position;
            nextCell.objectOnCell = head;
        }
        
        private void MovePart(CellObject part, CellObject nextPart, bool clearPartCell)
        {
            ref var partCell = ref m_cells[part.positionOnGrid.x][part.positionOnGrid.y];
            if (clearPartCell)
            {
                partCell.objectOnCell = null;
            }
            part.positionOnGrid = nextPart.positionOnGrid;
            part.transform.localPosition = nextPart.transform.localPosition;
            
            ref var nextCell = ref m_cells[part.positionOnGrid.x][part.positionOnGrid.y];
            nextCell.objectOnCell = part;
        }

        public ref Cell GetDestinationCell(int deltaX, int deltaY)
        {
            var newPos = m_body.snakeHead.positionOnGrid + new Vector2Int(deltaX, deltaY);
            return ref GetCell(newPos);
        }
    }
}
