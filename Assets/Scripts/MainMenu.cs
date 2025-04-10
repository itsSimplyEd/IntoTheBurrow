/*****************************************************************************
// File Name : RueLife.cs
// Author : Edward Parra-Garcia
// Creation Date : March 31, 2025
//
// Brief Description : This code has functions for the menus to access.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
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
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
