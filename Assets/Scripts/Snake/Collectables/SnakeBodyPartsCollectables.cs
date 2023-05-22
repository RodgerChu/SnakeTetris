using System.Collections;
using General.Grid;
using General.Grid.Objects;
using Snake.Grid;
using Snake.Parts;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Snake.Collectables
{
    public class SnakeBodyPartsCollectables : MonoBehaviour
    {
        [SerializeField] private CellObject m_partPrefab;
        [SerializeField] private CollectableSnakePart m_collectableSnakePartPrefab;
        [SerializeField] private int m_partsCountPerSpawn;
        
        [Inject] private SnakeGrid m_grid;
        [Inject] private SnakeBody m_snake;

        private Vector2Int m_newPartSpawnPosition = Vector2Int.left;
        private Random m_random = new Random();

        public void OnBeforeSnakeMovement(ref Cell destination)
        {
            if (destination.objectOnCell is CollectableSnakePart)
            {
                CollectSnakePart(ref destination);
            }
        }

        public void OnAfterSnakeMovement()
        {
            if (m_newPartSpawnPosition.x > -1)
            {
                SpawnBodyPart();
                m_newPartSpawnPosition.x = -1;
            }

            if (!m_grid.TryGetCellObject<CollectableSnakePart>(out _))
            {
                SpawnNewSnakeParts();
            }
        }

        private void SpawnNewSnakeParts()
        {
            var emptyCells = m_grid.GetFreeCellsCoords();
            for (int i = 0; i < m_partsCountPerSpawn && i < emptyCells.Count; i++)
            {
                var randomCellInd = m_random.Next(0, emptyCells.Count);
                var cellCoords = emptyCells[randomCellInd];
                var obj = Instantiate(m_collectableSnakePartPrefab, m_grid.transform.parent);
                ref var cell = ref m_grid.GetCell(cellCoords);
                cell.objectOnCell = obj;
                obj.transform.position = cell.position;
                emptyCells.RemoveAt(randomCellInd);
            }
        }

        private void CollectSnakePart(ref Cell cellWithPart)
        {
            var obj = cellWithPart.objectOnCell as Component;
            Destroy(obj.gameObject);
            cellWithPart.objectOnCell = null;
            
            var lastSnakePart = m_snake.snakeParts.Count == 0 
                ? m_snake.snakeHead 
                : m_snake.snakeParts[^1];
            m_newPartSpawnPosition = lastSnakePart.positionOnGrid;
        }

        private void SpawnBodyPart()
        {
            var part = Instantiate(m_partPrefab, m_snake.transform.parent);
            m_snake.AddSnakePart(part);
            ref var cell = ref m_grid.GetCell(m_newPartSpawnPosition);
            part.transform.position = cell.position;
            part.positionOnGrid = m_newPartSpawnPosition;
            cell.objectOnCell = part;
        }
    }
}
