using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    public AudioSource buttonPressSound;
    public AudioSource buttonHoveredSound;

    public void buttonHovered() 
    {
        buttonHoveredSound.Play(0);
    }

    public void buttonPressed()
    {
        buttonPressSound.Play(0);
        Debug.Log("Button pressed!");
        loadSceneByName(sceneName);
    }

    public void quitGame()
    {
        Debug.Log("Game quit.");
        Application.Quit();
    }

    public void loadSceneByName(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
