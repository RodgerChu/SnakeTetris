using System;

namespace ResourcesManagement.Abstraction
{
    public abstract class BaseSceneLoader
    {
        public abstract void LoadScene(string sceneName, Action callback);
    }
}
