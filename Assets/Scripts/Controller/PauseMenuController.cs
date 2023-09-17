using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public AudioMixer audioMixer;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;

        // Mute all sounds
        if (audioMixer != null)
        {
            audioMixer.SetFloat("masterVolume", -80f);
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Unpause the game
        isPaused = false;

        // Unmute all sounds (set the volume back to a normal level)
        if (audioMixer != null)
        {
            audioMixer.SetFloat("masterVolume", 0f); 
        }
    }


    public void Restart()
    {
        //ResetPlayerCharacter();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (audioMixer != null)
        {
            audioMixer.SetFloat("masterVolume", 0f);
        }
    }

    public void ExitToLevelPanel()
    {
        Time.timeScale = 1f; // Unpause the game
        SceneManager.LoadScene("Main Menu");
    }
}
