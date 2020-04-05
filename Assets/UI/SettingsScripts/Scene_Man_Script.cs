using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Man_Script : MonoBehaviour
{
    public void Load(string scene)
    {
        Debug.Log(scene);
        if (scene=="this")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void TimeStopper(bool x)
    {
        if (x)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}

