namespace General
{
    public abstract class Singleton<T> where T : new()
    {
        public static bool hasInstance
        {
            get { return m_instance != null; }
        }

        public static T instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new T();
                }

                return m_instance;
            }
        }


        private static T m_instance;

        public Singleton()
        {
            if (m_instance != null)
            {
                return;
            }

            OnInitialization();
        }

        protected virtual void OnInitialization()
        {
        }
    }
}