using System;
using System.Collections;
using Animations;
using General.Grid.Objects;
using Snake.Collectables;
using Snake.Grid;
using Snake.Parts;
using UnityEngine;
using Zenject;

namespace Snake.Movement
{
    public class SnakeMovement: MonoBehaviour
    {
        [Inject] private SnakeBody m_body;
        [Inject] private SnakeGrid m_grid;
        [Inject] private IMovementInput m_movementInput;
        [Inject] private SnakeBodyPartsCollectables m_collectables;
        [Inject] private CharacterAnimationController m_animationController;

        private float m_cooldown;
        
        private Vector2Int m_snakeHeadPosition;

        public void Update()
        {
            if (!m_animationController.CanMove())
            {
                return;
            }
            
            var input = m_movementInput.GetInput();
            if (input.x != 0 && m_grid.CanMoveX(input.x))
            {
                m_collectables.OnBeforeSnakeMovement(ref m_grid.GetDestinationCell(input.x, 0));
                MoveSnakeX(input.x);
                m_animationController.PlayAnimation(input.x < 0 ? AnimationType.JumpRight : AnimationType.JumpLeft);
                
                m_cooldown = .3f;
            }
            else if (input.y != 0 && m_grid.CanMoveY(input.y))
            {
                m_collectables.OnBeforeSnakeMovement(ref m_grid.GetDestinationCell(0, input.y));
                MoveSnakeY(input.y);
                m_animationController.PlayAnimation(input.y < 0 ? AnimationType.JumpUp : AnimationType.JumpDown);
                m_cooldown = .3f;
            }
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
            
            ref var nextCell = ref m_grid.GetCell(head.positionOnGrid);
            
            StartCoroutine(TranslateCoroutine(head.transform, nextCell.position, () =>
            {
                m_collectables.OnAfterSnakeHeadMovement();
            }));
            
            nextCell.objectOnCell = head;
            
            m_collectables.CheckSnakeNewBodyPartSpawn();
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
            
            ref var nextCell = ref m_grid.GetCell(head.positionOnGrid);
            
            StartCoroutine(TranslateCoroutine(head.transform, nextCell.position, () =>
            {
                m_collectables.OnAfterSnakeHeadMovement();
            }));
            
            nextCell.objectOnCell = head;

            m_collectables.CheckSnakeNewBodyPartSpawn();
        }
        
        private void MovePart(CellObject part, CellObject nextPart, bool clearPartCell)
        {
            ref var partCell = ref m_grid.GetCell(part.positionOnGrid);
            if (clearPartCell)
            {
                partCell.objectOnCell = null;
            }
            part.positionOnGrid = nextPart.positionOnGrid;

            part.StartCoroutine(TranslateCoroutine(part.transform, nextPart.transform.position));

            ref var nextCell = ref m_grid.GetCell(part.positionOnGrid);
            nextCell.objectOnCell = part;
        }

        private IEnumerator TranslateCoroutine(Transform obj, Vector3 destination, Action onComplete = null)
        {
            var startPos = obj.position;
            var multiplier = 1f / m_animationController.moveAnimationLength;
            var time = 0f;
            do
            {
                yield return null;
                time += Time.deltaTime * multiplier;
                obj.position = Vector3.Lerp(startPos, destination, time);
            } while (time < 1f);

            obj.position = destination;
            
            onComplete?.Invoke();
        }
    }
}
