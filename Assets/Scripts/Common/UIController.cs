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
            var inventoryPanel = GameObject.Find("Canvas").transform.Find("InventoryPanel").gameObject;
            inventoryPanel.SetActive(true);
        }

        public void CloseInventoryButton()
        {
            var inventoryPanel = GameObject.Find("Canvas/InventoryPanel");
            inventoryPanel.SetActive(false);
        }
    }
}
