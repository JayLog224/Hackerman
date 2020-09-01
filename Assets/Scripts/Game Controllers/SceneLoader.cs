using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(sceneName);

        if (sceneName == "Gameplay" && currentScene.name == "MainMenu")
        {

        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
