using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    private bool isPaused = false; // Tracks if the game is paused

    void Start()
    {
        // Ensure the pause menu is hidden at the start and time scale is normal
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Check for the pause input (usually the "Escape" key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    // Pauses the game and shows the pause menu
    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Stop the game's tick
    }

    // Resumes the game from its current position
    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false); // Make sure to hide settings menu if open
        Time.timeScale = 1f; // Resume the game's tick
    }

    // Switches to the settings menu
    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    // Goes back to the pause menu from settings
    public void ExitSettings()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    // Loads the main menu scene
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Ensure time is normal when switching scenes
        SceneManager.LoadScene("MainMenu");
    }
   
}
