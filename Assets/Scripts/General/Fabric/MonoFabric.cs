using General.Pool;
using UnityEngine;
using Zenject;

namespace General.Fabric
{
    public class MonoFabric
    {
        [Inject] private MonoPool m_pool;

        public T Create<T>(T prefab) where T : MonoBehaviour
        {
            if (m_pool.TryGetFromPool(out T obj))
            {
                return obj;
            }

            return Object.Instantiate(prefab);
        }

        public void Destroy<T>(T @object) where T : MonoBehaviour
        {
            if (!m_pool.TryReturnToPool(@object))
            {
                Object.Destroy(@object.gameObject);
            }
        }
    }
}