using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class FinishController : MonoBehaviour
{
    public GameObject completedDialog;
    public AudioSource finishSound;
    private bool levelCompleted = false;
    void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !levelCompleted)
        {
            finishSound.Play();
            levelCompleted = true;
            ShowCompletedDialog();
        }
    }

    private void ShowCompletedDialog()
    {
        completedDialog.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f; // Unpause the game
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Level " + (currentLevelIndex + 1));
    }

    public void ExitToLevelPanel()
    {
        Time.timeScale = 1f; // Unpause the game
        SceneManager.LoadScene("Main Menu");
    }

}
