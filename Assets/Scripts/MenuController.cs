using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("master") && PlayerPrefs.HasKey("music") && PlayerPrefs.HasKey("SFX"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }

        resolutions=Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for(int i=0;i<resolutions.Length;i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    [Header("Volume Setting")]
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private TMP_Text masterVolumeTextValue;
    [SerializeField] private TMP_Text musicVolumeTextValue;
    [SerializeField] private TMP_Text sfxVolumeTextValue;
    [SerializeField] private Slider masterVolumeSlider=null;
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Slider sfxVolumeSlider = null;
    [SerializeField] private float defaultMasterVolume = 0.7f;
    [SerializeField] private float defaultMusicVolume = 0.7f;
    [SerializeField] private float defaultSFXVolume = 0.3f;
    public void SetMasterVolume()
    {
        float masterVolume = masterVolumeSlider.value;
        myMixer.SetFloat("masterVolume", Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        masterVolumeTextValue.text = masterVolume.ToString("0.0");
    }
    public void SetMusicVolume()
    {
        float musicVolume = musicVolumeSlider.value;
        myMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        musicVolumeTextValue.text = musicVolume.ToString("0.0");
    }
    public void SetSFXVolume()
    {
        float sfxVolume = sfxVolumeSlider.value;
        myMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        sfxVolumeTextValue.text = sfxVolume.ToString("0.0");
    }
    public void LoadVolume()
    {
        Debug.Log("Loading volume ......");
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
    public void ResetButton(string MenuType)
    {
        if (MenuType == "Graphics")
        {
            //Reset brightness value
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }

        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultMasterVolume;
            masterVolumeSlider.value = defaultMasterVolume;
            musicVolumeSlider.value = defaultMusicVolume;
            sfxVolumeSlider.value = defaultSFXVolume;
            masterVolumeTextValue.text = defaultMasterVolume.ToString("0.0");
            musicVolumeTextValue.text = defaultMusicVolume.ToString("0.0");
            sfxVolumeTextValue.text = defaultSFXVolume.ToString("0.0");
        }
    }

    [Header("Levels To Load")]
    //public string newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;
    public void NewGameDialogYes(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            //PlayerPrefs.SetString("SavedLevel,1");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Debug.Log("Quit !");
        Application.Quit();
    }

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int qualityLevel;
    private bool isFullScreen;
    private float brightnessLevel;

    public void SetBrightness(float brightness)
    {
        brightnessLevel=brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool FullScreen) 
    {
        isFullScreen = FullScreen;    
    }

    public void SetQuality(int qualityIndex)
    {
        qualityLevel=qualityIndex;
    }

    public void GraphicsApply()
    {
        //Change your brightness with your post processing or whatever it is
        PlayerPrefs.SetFloat("masterBrightness", brightnessLevel);

        PlayerPrefs.SetInt("masterQuality", qualityLevel);
        QualitySettings.SetQualityLevel(qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen",(isFullScreen ? 1:0));
        Screen.fullScreen = isFullScreen;
    }

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

}
