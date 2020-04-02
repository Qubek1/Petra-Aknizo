using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Settings_Script : MonoBehaviour
{
    public Button back;
    //public Slider volumeSlider, musicSlider, fxSlider;
    public AudioMixer audioMixer;
    //public Text volumeValue,musicValue,fxValue;
    //public Button full_screen;
    //public Text fs_on_off;
    //public Settings settings;
    // Start is called before the first frame update
    void Start()
    {
        //volumeValue.text = (int)(settings.masterVolVal * 100) + "%";
        //musicValue.text = (int)(settings.musicVolVal * 100) + "%";
        //fxValue.text = (int)(settings.fxVolVal * 100) + "%";
        //full_screen.onClick.AddListener(SetFullscreen);
    }
    void Load(string scene)
    {
        Debug.Log(scene);
        SceneManager.LoadScene(scene);
    }


}
