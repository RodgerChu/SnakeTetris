using System;
using UnityEngine;

namespace UI.View.Animation
{
    public enum ViewAnimation
    {
        Show, Idle, Hide
    }

    public static class ViewAnimationExtension
    {
        private static int m_showAnimationHash = Animator.StringToHash("Show");
        private static int m_idleAnimationHash = Animator.StringToHash("Idle");
        private static int m_hideAnimationHash = Animator.StringToHash("Hide");
        
        public static int ToAnimationHash(this ViewAnimation animation)
        {
            return animation switch
            {
                ViewAnimation.Hide => m_hideAnimationHash,
                ViewAnimation.Idle => m_idleAnimationHash,
                _ => m_showAnimationHash
            };
        }
    }
    
    public class ViewAnimationController : MonoBehaviour
    {
        public Action<ViewAnimation> onAnimationFinished;

        [SerializeField] private Animator m_animator;

        public void PlayAnimation(ViewAnimation animation)
        {
            m_animator.SetTrigger(animation.ToAnimationHash());
        }
        
        // ANIMATION EVENT

        public void OnAnimationFinished(ViewAnimation animation)
        {
            onAnimationFinished?.Invoke(animation);
        }
    }
}
