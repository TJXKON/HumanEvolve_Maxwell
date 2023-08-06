using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Ensure that the unlockedLevel value doesn't exceed the number of buttons.
        unlockedLevel = Mathf.Clamp(unlockedLevel, 1, buttons.Length);

        for (int i = 0; i < buttons.Length; i++)
        {
            // Enable buttons up to the unlockedLevel index (exclusive).
            buttons[i].interactable = i < unlockedLevel;
        }
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }
}

