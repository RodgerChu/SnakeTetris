using System;
using ResourcesManagement.Abstraction;
using UnityEngine.SceneManagement;

namespace ResourcesManagement.Concrete
{
    public class UnityResourcesSceneLoader : BaseSceneLoader
    {
        public override void LoadScene(string sceneName, Action callback)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).completed += _ =>
            {
                callback();
            };
        }
    }
}
