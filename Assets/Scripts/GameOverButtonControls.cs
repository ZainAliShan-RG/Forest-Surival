using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonControls : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log($"Restart Button Clicked!");
        SceneManager.LoadScene(sceneName:"GamePlay");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
}
