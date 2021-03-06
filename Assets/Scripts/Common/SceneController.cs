using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// Load next scene using scenes in build.
        /// </summary>
        public void NextScene()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            int currentScene = SceneManager.GetActiveScene().buildIndex;
        
            if (currentScene < sceneCount - 1)
            {
                SceneManager.LoadScene(currentScene + 1);
            }
            else
            {
                SceneManager.LoadScene(SceneBuildIndex.Level1);
            }
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(SceneBuildIndex.MainMenu);
        }

        public void RestartCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadScene(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
