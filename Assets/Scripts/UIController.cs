using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;

    public void OnStartGameButton()
    {
        sceneController.NextScene();
    }

    public void OnLoadMainMenu()
    {
        sceneController.LoadMainMenu();
    }
}
