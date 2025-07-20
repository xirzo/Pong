using System;
using UnityEngine.SceneManagement;

namespace Pong.Domain.Scenes
{
    public class SceneLoader
    {
        public void LoadNext()
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
            {
                throw new IndexOutOfRangeException("Already loaded last scene");
            }
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
