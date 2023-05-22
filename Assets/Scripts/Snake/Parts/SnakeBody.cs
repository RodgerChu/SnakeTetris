using System.Collections.Generic;
using General.Grid.Objects;
using UnityEngine;

namespace Snake.Parts
{
    public class SnakeBody: MonoBehaviour
    {
        public CellObject snakeHead;
        public List<CellObject> snakeParts = new List<CellObject>(4);

        public void AddSnakePart(CellObject part)
        {
            snakeParts.Add(part);
        }

        public CellObject GetLastPart()
        {
            return snakeParts.Count == 0 ? snakeHead : snakeParts[^1];
        }
    }
}
