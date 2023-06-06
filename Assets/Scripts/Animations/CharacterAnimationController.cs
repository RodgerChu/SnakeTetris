using System;
using System.Collections;
using UnityEngine;

namespace Animations
{
    public enum AnimationType
    {
        IdleUp,
        IdleDown,
        IdleLeft,
        IdleRight,
        
        JumpUp,
        JumpDown,
        JumpLeft,
        JumpRight
    }

    public static class AnimationTypeExtensions
    {
        private static int m_idleTopHash = Animator.StringToHash(AnimationType.IdleUp.ToString());
        private static int m_idleDownHash = Animator.StringToHash(AnimationType.IdleDown.ToString());
        private static int m_idleLeftHash = Animator.StringToHash(AnimationType.IdleLeft.ToString());
        private static int m_idleRightHash = Animator.StringToHash(AnimationType.IdleRight.ToString());
        
        private static int m_moveTopHash = Animator.StringToHash(AnimationType.JumpUp.ToString());
        private static int m_moveDownHash = Animator.StringToHash(AnimationType.JumpDown.ToString());
        private static int m_moveLeftHash = Animator.StringToHash(AnimationType.JumpLeft.ToString());
        private static int m_moveRightHash = Animator.StringToHash(AnimationType.JumpRight.ToString());
        
        public static int ToAnimationHash(this AnimationType type)
        {
            return type switch
            {
                AnimationType.IdleDown => m_idleDownHash,
                AnimationType.IdleLeft => m_idleLeftHash,
                AnimationType.IdleRight => m_idleRightHash,
                AnimationType.IdleUp => m_idleTopHash,

                AnimationType.JumpDown => m_moveDownHash,
                AnimationType.JumpLeft => m_moveLeftHash,
                AnimationType.JumpRight => m_moveRightHash,
                AnimationType.JumpUp => m_moveTopHash,

                _ => 0
            };
        }

        public static bool IsTop(this AnimationType type)
        {
            return type is AnimationType.IdleUp or AnimationType.JumpUp;
        }

        public static bool IsLeft(this AnimationType type)
        {
            return type is AnimationType.IdleLeft or AnimationType.JumpLeft;
        }

        public static bool IsMovementType(this AnimationType type)
        {
            return type is AnimationType.JumpDown or AnimationType.JumpLeft or AnimationType.JumpRight or AnimationType.JumpUp;
        }
    }
    
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;

        private AnimationType m_lastAnimationType;
        public float moveAnimationLength { get; private set; }

        private bool m_canMove = true;

        private void Awake()
        {
            var movementClipName = AnimationType.JumpDown.ToString();
            foreach (var animationClip in m_animator.runtimeAnimatorController.animationClips)
            {
                if (animationClip.name.EndsWith(movementClipName))
                {
                    moveAnimationLength = animationClip.length;
                    break;
                }
            }
        }

        public bool CanMove()
        {
            return m_canMove;
        }

        public void PlayAnimation(AnimationType animationType)
        {
            if (m_lastAnimationType != animationType && m_lastAnimationType.IsLeft() != animationType.IsLeft())
            {
                var tr = transform;
                var scale = tr.localScale;
                scale.x *= -1;
                tr.localScale = scale;
            }
            
            m_animator.Play(animationType.ToAnimationHash());
            m_lastAnimationType = animationType;

            if (animationType.IsMovementType())
            {
                StartCoroutine(BlockMovementForAnimation(animationType));
            }
        }
        
        private IEnumerator BlockMovementForAnimation(AnimationType animation)
        {
            m_canMove = false;
            
            var elapsedTime = 0f;
            do
            {
                yield return null;
                elapsedTime += Time.deltaTime;
            } while (elapsedTime < moveAnimationLength);

            m_canMove = true;
        }
    }
}
