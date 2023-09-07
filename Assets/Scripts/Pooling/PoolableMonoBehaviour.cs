using UnityEngine;

namespace GameCore.Pooling
{
    public abstract class PoolableMonoBehaviour : MonoBehaviour
    {
        public virtual void OnMovedToPool() { }
        public virtual void OnMovedFromPool() { }
    }
}
