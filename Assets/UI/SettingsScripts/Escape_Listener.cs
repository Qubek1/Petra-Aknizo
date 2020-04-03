using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape_Listener : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausemenu,settingsCanvas;
    public GameObject pausebutton;
    public Scene_Man_Script sms;
    public bool paused;
    void Start()
    {
        paused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            DoPause();
        }
        
    }
    public void SetPaused(bool x)
    {
        paused = x;
        DoPause();
    }
    public void DoPause()
    {
        pausemenu.SetActive(paused);
        sms.TimeStopper(!paused);
        if (!paused)
        {
            settingsCanvas.SetActive(false);
            pausebutton.SetActive(true);
        }
    }

}
