namespace GameCore.Debug
{
    public interface ILogger
    {
        void Log(string str);
        void LogWarning(string str);
        void LogError(string str);
    }
}
