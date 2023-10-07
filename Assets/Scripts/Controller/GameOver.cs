using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameOver: MonoBehaviour
{
    public GameObject gameOverDialog;
    public AudioSource gameOverSound;
    public AudioMixer audioMixer;
    public void PlayMusic()
    {
        gameOverSound = GetComponent<AudioSource>();
        gameOverSound.Play();
    }
   
    public void ShowGameOverDialog()
    {
        gameOverDialog.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void TryAgainLevel()
    {
        FinishController tryAgain = FindObjectOfType<FinishController>();
        if (tryAgain != null)
        {
            tryAgain.RestartLevel();
        }
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Unpause the game
        SceneManager.LoadScene("Main Menu");
    }

}
