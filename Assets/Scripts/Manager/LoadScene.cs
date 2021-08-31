using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Util;

namespace Game.Manager
{
    public class LoadScene : Singleton<LoadScene>
    {
        public void Load(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

