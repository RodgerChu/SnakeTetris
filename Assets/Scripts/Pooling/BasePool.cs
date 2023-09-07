using System.Collections.Generic;
using Zenject;

namespace GameCore.Pooling
{
    public class Pool<TBase>
    {
        [Inject] private DiContainer m_container;
        
        private List<TBase> m_pool = new();

        public TRequested Get<TRequested>() where TRequested: TBase, new()
        {
            foreach (var element in m_pool)
            {
                if (element is TRequested requested)
                {
                    m_pool.Remove(requested);
                    return requested;
                }
            }
            
            return m_container.Instantiate<TRequested>();
        }

        public void Return(TBase obj)
        {
            m_pool.Add(obj);
        }
    }
}
