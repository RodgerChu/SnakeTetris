using System;
using System.Collections;
using General.Grid.Objects;
using UnityEngine;

namespace Animations
{
    public class SnakeToTetrisAnimation: MonoBehaviour
    {
        [SerializeField] private float m_objectMoveSpeed;
        [SerializeField] private float m_thresholdTime;

        public void TranslateObjectToPosition(CellObject cellObject, Vector3 targetPosition, Action onAnimationComplete = null)
        {
            cellObject.StartCoroutine(AnimationCoroutine(cellObject, targetPosition, onAnimationComplete));
        }
        
        private IEnumerator AnimationCoroutine(CellObject objectToMove, Vector3 targetPosition, Action onAnimationComplete = null)
        {
            var objToMoveTransform = objectToMove.transform;
            var position = objToMoveTransform.position;
            
            var direction = targetPosition - position;
            direction = direction.normalized;
            
            var unitsPerSecond = m_objectMoveSpeed;
            var elapsedTime = 0f;
            var timeToMove = Vector3.Distance(position, targetPosition) / unitsPerSecond;
            do
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                position += direction * (unitsPerSecond * Time.deltaTime);
                objToMoveTransform.position = position;
            } while (elapsedTime < timeToMove);

            objToMoveTransform.position = targetPosition;

            elapsedTime = 0f;
            do
            {
                yield return null;
                elapsedTime += Time.deltaTime;
            } while (elapsedTime < m_thresholdTime);
            
            onAnimationComplete?.Invoke();
        }
    }
}