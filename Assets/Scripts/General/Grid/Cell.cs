using General.Grid.Objects;
using UnityEngine;

namespace General.Grid
{
    public struct Cell
    {
        public readonly Vector2 position;
        public CellObject objectOnCell;

        public Cell(Vector2 position)
        {
            this.position = position;
            objectOnCell = null;
        }
    }
}
