namespace GameCore.Debug
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string str)
        {
            UnityEngine.Debug.Log(str);
        }

        public void LogWarning(string str)
        {
            UnityEngine.Debug.LogWarning(str);
        }

        public void LogError(string str)
        {
            UnityEngine.Debug.LogError(str);
        }
    }
}
