using Animations;
using General.Grid;
using General.Grid.Objects;
using Messaging;
using Tetris.Messaging.Events;
using UnityEngine;

namespace Tetris.Grid
{
    public class TetrisGrid: GameGrid
    {
        [SerializeField] private SnakeToTetrisAnimation m_snakeToTetrisAnimation;
        
        public bool TryGetFreeCellCoordsAtRow(int row, out Vector2Int coords)
        {
            for (int i = m_cells.Length - 1; i >= 0; i--)
            {
                ref var cell = ref m_cells[i][row];
                if (cell.objectOnCell is null)
                {
                    coords = new Vector2Int(i, row);
                    return true;
                }
            }

            coords = default;
            return false;
        }

        public void MoveObjectToCell(CellObject cellObject, Vector2Int coords)
        {
            ref var cell = ref m_cells[coords.x][coords.y];
            cell.objectOnCell = cellObject;
            m_snakeToTetrisAnimation.TranslateObjectToPosition(cellObject, cell.position, OnObjectReachedCell);
        }

        private void OnObjectReachedCell()
        {
            CheckTetrisColumn();
        }

        private void CheckTetrisColumn()
        {
            for (int i = 0; i < m_cells.Length; i++)
            {
                for (int j = 0; j < m_cells[i].Length; j++)
                {
                    ref var cell = ref m_cells[i][j];
                    if (cell.objectOnCell is null)
                    {
                        goto ColumnNotFull;
                    }
                }

                CollectColumn(i);
                
                ColumnNotFull:;
            }
        }

        private void CollectColumn(int column)
        {
            for (int i = 0; i < m_cells[column].Length; i++)
            {
                ref var cell = ref m_cells[column][i];
                cell.objectOnCell.StopAllCoroutines();
                Destroy(cell.objectOnCell.gameObject);
                cell.objectOnCell = null;
                MoveRowObjectsToColumn(column, i);
            }
            
            EventBuss.instance.FireEvent(new TetrisColumnCollected());
        }

        private void MoveRowObjectsToColumn(int column, int row)
        {
            for (int i = column - 1; i >= 0; i--)
            {
                ref var cell = ref m_cells[i][row];
                if (cell.objectOnCell is null)
                {
                    return;
                }
                
                var obj = cell.objectOnCell;
                cell.objectOnCell = null;
                MoveObjectToCell(obj, new Vector2Int(i + 1, row));
            }
        }
    }
}