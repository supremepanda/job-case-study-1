using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;

    public void OnNextGameButton()
    {
        sceneController.NextScene();
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
