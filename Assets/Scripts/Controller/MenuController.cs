using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

public class MainMenu : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterVolumeSlider = null;
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Slider sfxVolumeSlider = null;
    [SerializeField] private TMP_Text masterVolumeTextValue;
    [SerializeField] private TMP_Text musicVolumeTextValue;
    [SerializeField] private TMP_Text sfxVolumeTextValue;
    [SerializeField] private float defaultMasterVolume = 0.7f;
    [SerializeField] private float defaultMusicVolume = 0.7f;
    [SerializeField] private float defaultSFXVolume = 0.3f;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [Header("Graphics Settings")]
    public Slider brightnessSlider;
    public TMP_Text brightnessTextValue;
    public float defaultBrightness = 1.0f;
    public PostProcessProfile brightness;
    private AutoExposure exposure;
    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;
    private int qualityLevel;
    private bool isFullScreen;
    private float brightnessLevel;

    [Header("Gameplay Settings")]
    [SerializeField] private Slider gameSpeedSlider = null;
    [SerializeField] private TMP_Text gameSpeedTextValue;
    [SerializeField] private float defaultGameSpeed = 1.0f;


    [Header("Levels To Load")]
    public GameObject LevelPanel;


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
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
            SetMasterVolume();
        }
        if (masterVolumeSlider != null)
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
            SetMusicVolume();
        }
        if (masterVolumeSlider != null)
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            SetSFXVolume();
        }
    }
    public void ResetButton(string MenuType)
    {
        if (MenuType == "Gameplay")
        {
            gameSpeedSlider.value = defaultGameSpeed;
            gameSpeedTextValue.text = defaultGameSpeed.ToString("0.0");
        }

        if (MenuType == "Graphics")
        {
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
 
    public void NewGameDialogYes(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }

    public void LoadGameDialogYes()
    {
        ActivateLevelPanel();
    }

    public void ExitButton()
    {
        Debug.Log("Quit !");
        Application.Quit();
    }

    public void SetBrightness(float brightnessValue)
    {
        if (exposure != null)
        {
            exposure.keyValue.value = brightnessValue;
            brightnessTextValue.text = brightnessValue.ToString("0.0");

            // Save brightness value to PlayerPrefs
            PlayerPrefs.SetFloat("masterBrightness", brightnessValue);
        }
    }

    private void LoadBrightness()
    {
        if (PlayerPrefs.HasKey("masterBrightness"))
        {
            float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

            brightnessTextValue.text = localBrightness.ToString("0.0");
            brightnessSlider.value = localBrightness;
        }
        else
        {
            brightnessTextValue.text = defaultBrightness.ToString("0.0");
            brightnessSlider.value = defaultBrightness;
            PlayerPrefs.SetFloat("masterBrightness", defaultBrightness);
        }
    }



    public void SetFullScreen(bool FullScreen)
    {
        isFullScreen = FullScreen;
    }

    public void SetQualityLevel(int qualityIndex)
    {
        qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetInt("masterQuality", qualityLevel);
        QualitySettings.SetQualityLevel(qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen", (isFullScreen ? 1 : 0));
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    // Handle changes to the game speed slider
    public void OnGameSpeedChanged(float newSpeed)
    {
        // Update the game speed and save it to PlayerPrefs
        GameManager.gameSpeed = newSpeed;
        SaveGameSpeed();

        // Update the game speed text value
        UpdateGameSpeedText(newSpeed);
    }

    // Update the game speed text value
    private void UpdateGameSpeedText(float speed)
    {
        gameSpeedTextValue.text = speed.ToString("0.0");
    }

    // Save the game speed to PlayerPrefs
    private void SaveGameSpeed()
    {
        PlayerPrefs.SetFloat("gameSpeed", GameManager.gameSpeed);
    }

    // Load the game speed from PlayerPrefs
    private void LoadGameSpeed()
    {
        if (PlayerPrefs.HasKey("gameSpeed"))
        {
            GameManager.gameSpeed = PlayerPrefs.GetFloat("gameSpeed");
        }
        else
        {
            GameManager.gameSpeed = defaultGameSpeed;
            PlayerPrefs.SetFloat("gameSpeed", defaultGameSpeed);
        }
    }

    private void Start()
    {
        LoadVolume();
        LoadGameSpeed();

        // Set the game speed slider's initial value
        gameSpeedSlider.value = GameManager.gameSpeed;
        UpdateGameSpeedText(GameManager.gameSpeed);

        exposure = brightness.GetSetting<AutoExposure>();

        // Load brightness value from PlayerPrefs
        float brightnessValue = PlayerPrefs.GetFloat("masterBrightness", defaultBrightness);

        if (exposure != null)
        {
            exposure.keyValue.value = brightnessValue;
            brightnessSlider.value = brightnessValue;
            brightnessTextValue.text = brightnessValue.ToString("0.0");
        }

        qualityDropdown.onValueChanged.AddListener(SetQualityLevel);

        resolutions = Screen.resolutions;
        if (resolutionDropdown != null)
        {
            resolutionDropdown.ClearOptions(); // Clear existing options in the resolutionDropdown
        }

        // Initialize the options list
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        // Populate the options list with resolution options
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Add the new options list to the resolutionDropdown
        if (resolutionDropdown != null)
        {
            resolutionDropdown.AddOptions(options);
        }

        // Set the value and refresh the shown value of the resolutionDropdown
        if (resolutionDropdown != null)
        {
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }

    public void ActivateLevelPanel()
    {
        Debug.Log("Level Panel Called");
        LevelPanel.SetActive(true);
    }


}