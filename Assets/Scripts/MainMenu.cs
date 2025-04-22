/*****************************************************************************
// File Name : MainMenu.cs
// Author : Edward Parra-Garcia
// Creation Date : March 31, 2025
//
// Brief Description : This code has functions for the menus to access and pauses the game. Also allows the pauseMenu
                       to be accessed.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;

    /// <summary>
    /// Loads the first scene/level
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Quits the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Pauses the scene when the bool is set to true and pops up the pauseMenu and then unpauses and gets rid of the 
    /// pauseMenu when the bool is set to false
    /// </summary>
    public void ResumeGame()
    {
        if (isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            isPaused = !isPaused;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    /// <summary>
    /// Loads scene 0 (mainMenu)
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
