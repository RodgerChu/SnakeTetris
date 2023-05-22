using System;
using System.Collections.Generic;
using UnityEngine;

namespace General.Pool
{
    [Serializable]
    public struct PoolInfo
    {
        public MonoBehaviour prefab;
        public int prewarmCount;
        public int maxCount;

        [NonSerialized]
        public List<MonoBehaviour> pooledObjects;
    }
    
    public class MonoPool: MonoBehaviour
    {
        [SerializeField] private PoolInfo[] m_info;

        private void Awake()
        {
            for (int i = 0; i < m_info.Length; i++)
            {
                ref var info = ref m_info[i];
                info.pooledObjects = new List<MonoBehaviour>(info.maxCount);
                for (int j = 0; j < info.prewarmCount; j++)
                {
                    info.pooledObjects.Add(Instantiate(info.prefab));
                }
            }
        }

        public bool TryGetFromPool<T>(out T @object) where T : MonoBehaviour
        {
            foreach (var poolInfo in m_info)
            {
                if (poolInfo.prefab is T tObj)
                {
                    if (poolInfo.pooledObjects.Count > 0)
                    {
                        @object = poolInfo.pooledObjects[^1] as T;
                        return true;
                    }
                }
            }

            @object = null;
            return false;
        }

        public bool TryReturnToPool<T>(T @object) where T : MonoBehaviour
        {
            foreach (var info in m_info)
            {
                if (info.prefab is T && info.pooledObjects.Count < info.maxCount)
                {
                    info.pooledObjects.Add(@object);
                    return true;
                }
            }

            return false;
        }
    }
}
