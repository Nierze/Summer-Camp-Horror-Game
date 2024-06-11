using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class GeneralSettingsScript : MonoBehaviour
{
    public AudioMixer mixer;
    float volume;

    public TMP_Dropdown resoDropdown;

    Resolution[] resolutions;
    private void Start()
    {
        resolutions = Screen.resolutions;
        resoDropdown.ClearOptions();
        List<string> list = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            list.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resoDropdown.AddOptions(list);
        resoDropdown.value = currentResolutionIndex;
        resoDropdown.RefreshShownValue();
    }


    public void SetMasterVolume(float volume)
    {
        mixer.SetFloat("Master", volume);
    }

    public void SetBGMVolume(float volume)
    {
        mixer.SetFloat("BGMVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", volume);
    }

    public void setQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullScreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }

    public void SetResolution(int resoIndex)
    {
        Resolution resolution = resolutions[resoIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
