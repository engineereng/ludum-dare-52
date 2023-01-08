using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    public void buttonPressed()
    {
        Debug.Log("Button pressed!");
        loadSceneByName(sceneName);
    }

    public void quitGame()
    {
        Debug.Log("Game quit.");
        Application.Quit();
    }

    private void loadSceneByName(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
