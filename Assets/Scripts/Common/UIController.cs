using UnityEngine;

namespace Common
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private SceneController sceneController;

        public void OnNextGameButton()
        {
            sceneController.NextScene();
        }

        public void OnDirectSceneButton(int sceneBuildIndex)
        {
            sceneController.LoadScene(sceneBuildIndex);
        }

        public void OnLoadMainMenu()
        {
            sceneController.LoadMainMenu();
        }

        public void OnRestartGameButton()
        {
            sceneController.RestartCurrentScene();
        }

        public void OnInventoryButton()
        {
        
        }
    }
}
