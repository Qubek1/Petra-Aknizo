using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SAV : MonoBehaviour
{
    public AudioMixer mixer;
    public Text volumeValue;
    public string Path;
    public Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        float z,a;
        mixer.GetFloat(Path, out z);
        a = Mathf.Round((Mathf.Pow(10, z / 20) * 100));
        volumeValue.text = a+"%";
        slider.value = a / 100;
    }
    public void SetLevel(float sliderValue)
    {
        float x = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat(Path, x);
        volumeValue.text = (int)(sliderValue * 100)+"%";// 5/4*(80+(int)x) + "%";
        /*if (Path == "MasterVol")
        {
            settings.masterVolVal = sliderValue;
        }
        else if (Path == "MusicVol")
        {
            settings.musicVolVal = sliderValue;
        }
        else if (Path == "FXVol")
        {
            settings.fxVolVal = sliderValue;
        }*/
    }

}
