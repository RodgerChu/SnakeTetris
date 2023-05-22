using System.Collections;
using System.Collections.Generic;
using General.Grid.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace General.Grid
{
    public class GameGrid : GridLayoutGroup
    {
        protected Cell[][] m_cells;
        private List<Vector2Int> m_freeCells;

        protected override void Start()
        {
            base.Start();

            StartCoroutine(InitCellsCoroutine());
        }

        private IEnumerator InitCellsCoroutine()
        {
            yield return null;
            
            var tr = transform;
            var childrenCount = tr.childCount;

            var columns = m_Constraint == Constraint.FixedColumnCount
                ? m_ConstraintCount
                : childrenCount / m_ConstraintCount;
            var rows = m_Constraint == Constraint.FixedRowCount 
                ? m_ConstraintCount 
                : childrenCount / m_ConstraintCount;
            
            m_cells = new Cell[columns][];
            m_freeCells = new List<Vector2Int>(columns * rows);

            for (int i = 0; i < columns; i++)
            {
                var cellsRow = new Cell[rows];
                for (int j = 0; j < rows; j++)
                {
                    var goCell = tr.GetChild(j * columns + i);
                    var cell = new Cell(goCell.position);
                    cellsRow[j] = cell;
                }
                
                m_cells[i] = cellsRow;
            }
        }

        public ref Cell GetCell(Vector2Int coordinates)
        {
            return ref m_cells[coordinates.x][coordinates.y];
        }

        public List<Vector2Int> GetFreeCellsCoords()
        {
            m_freeCells.Clear();

            for (var i = 0; i < m_cells.Length; i++)
            {
                var cellsRows = m_cells[i];
                for (var j = 0; j < cellsRows.Length; j++)
                {
                    ref var cell = ref cellsRows[j];
                    if (cell.objectOnCell is null)
                    {
                        m_freeCells.Add(new Vector2Int(i, j));
                    }
                }
            }

            return m_freeCells;
        }

        public bool TryGetCellObject<T>(out T @object) where T : CellObject
        {
            @object = default;
            foreach (var cellsRow in m_cells)
            {
                foreach (var cell in cellsRow)
                {
                    if (cell.objectOnCell is T obj)
                    {
                        @object = obj;
                        return true;
                    }
                }
            }
            
            return false;
        }

        protected void ResetCells()
        {
            for (int i = 0; i < m_cells.Length; i++)
            {
                var cellsRow = m_cells[i];
                for (int j = 0; j < cellsRow.Length; j++)
                {
                    ref var cell = ref m_cells[i][j];
                    cell.objectOnCell = null;
                }
            }
        }
    }
}
