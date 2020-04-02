using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSR : MonoBehaviour
{
    // Set Screen Resolution
    Resolution[] reslist;
    public Dropdown resdrop, qualdrop;
    public Toggle fstogg;

    void Awake()
    {
        reslist = Screen.resolutions;
        resdrop.ClearOptions();
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for (int i=0; i<reslist.Length; i++)
        {
            string option = reslist[i].width + " x " + reslist[i].height + " : " + reslist[i].refreshRate + " Hz";
            options.Add(option);
            if (reslist[i].width==Screen.currentResolution.width && 
                reslist[i].height==Screen.currentResolution.height &&
                reslist[i].refreshRate==Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }
        resdrop.AddOptions(options);
        resdrop.value = currentResolutionIndex;
        resdrop.RefreshShownValue();
        qualdrop.value = QualitySettings.GetQualityLevel();
        qualdrop.RefreshShownValue();
        fstogg.isOn = Screen.fullScreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = reslist[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullscreen(bool fs)
    {
        Screen.fullScreen = fs;
    }
    public void SetQuality(int qualind) //quality index
    {
        QualitySettings.SetQualityLevel(qualind);
    }
}
