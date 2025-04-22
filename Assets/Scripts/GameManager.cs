/*****************************************************************************
// File Name : GameManager.cs
// Author : Edward Parra-Garcia
// Creation Date : March 31, 2025
//
// Brief Description : Quits the game and has the player return to main menu (after the timer reaches 0) 
                       when they find the last exit in level 4.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timer;

    /// <summary>
    /// Exits the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Starts the corutine TimerEnd()
    /// </summary>
    public void FinalExit()
    {
        StartCoroutine(TimerEnd());
    }
    /// <summary>
    /// Tells the script to wait for the amount of seconds (the timer) and then loads scene 0 (Main Menu)
    /// </summary>
    /// <returns></returns>
    IEnumerator TimerEnd()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(0);
    }
}
