using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void NextScene()
    {
        int sceneCount = SceneManager.sceneCount;
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
}
