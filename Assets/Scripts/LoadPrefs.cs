using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LoadPrefs : MonoBehaviour
{
    [Header("General Setting")]
    [SerializeField] private bool canUse = false;
   //[SerializeField] private MenuController menuController;

    [Header("Volume Setting")]
    [SerializeField] private TMP_Text masterVolumeTextValue = null;
    [SerializeField] private Slider masterVolumeSlider = null;

    [SerializeField] private TMP_Text musicVolumeTextValue = null;
    [SerializeField] private Slider musicVolumeSlider = null;

    [SerializeField] private TMP_Text sfxVolumeTextValue = null;
    [SerializeField] private Slider sfxVolumeSlider = null;


    [Header("Brightness Setting")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;

    [Header("Quality Level Setting")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Fullscreen Setting")]
    [SerializeField] private Toggle fullScreenToggle;

    private void Awake()
    {
        if(canUse)
        {
            if(PlayerPrefs.HasKey("master"))
            {
                float loacalMasterVolume = PlayerPrefs.GetFloat("masterVolume");
                float loacalMusicVolume = PlayerPrefs.GetFloat("musicVolume");
                float loacalSFXVolume = PlayerPrefs.GetFloat("sfxVolume");

                masterVolumeTextValue.text = loacalMasterVolume.ToString("0.0");
                musicVolumeTextValue.text = loacalMusicVolume.ToString("0.0");
                sfxVolumeTextValue.text = loacalSFXVolume.ToString("0.0");

                masterVolumeSlider.value = loacalMasterVolume;
                musicVolumeSlider.value = loacalMusicVolume;
                sfxVolumeSlider.value = loacalSFXVolume;

                AudioListener.volume = loacalMasterVolume;
                AudioListener.volume = loacalMusicVolume;
                AudioListener.volume = loacalSFXVolume;
            }
            else
            {
             //  menuController.ResetButton("Audio");
            }

            if(PlayerPrefs.HasKey("masterQuality"))
            {
                int localQuality = PlayerPrefs.GetInt("masterQuality");
                qualityDropdown.value = localQuality;   
                QualitySettings.SetQualityLevel(localQuality);
            }

            if (PlayerPrefs.HasKey("masterFullscreen"))
            {
                int localFullScreen = PlayerPrefs.GetInt("masterFullScreen");

                if(localFullScreen == 1)
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else
                {
                    Screen.fullScreen= false;
                    fullScreenToggle.isOn = false;
                }
            } 

            if(PlayerPrefs.HasKey("masterBrightness"))
            {
                float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                brightnessTextValue.text = localBrightness.ToString("0.0");
                brightnessSlider.value = localBrightness;
            }
            else
            {
                float defaultBrightness = 1.8f;
                brightnessTextValue.text = defaultBrightness.ToString("0.0");
                brightnessSlider.value = defaultBrightness;

                // Save the default brightness to PlayerPrefs
                PlayerPrefs.SetFloat("masterBrightness", defaultBrightness);
            }
        }
    }

}
