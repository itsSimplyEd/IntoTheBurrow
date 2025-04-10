/*****************************************************************************
// File Name : GameManager.cs
// Author : Edward Parra-Garcia
// Creation Date : March 31, 2025
//
// Brief Description : Quits the game and has the player return to main menu when they find the last exit in level 3.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timer;
    public void QuitGame()
    {
        Application.Quit();
    }
    public void FinalExit()
    {
        StartCoroutine(TimerEnd());
    }
    IEnumerator TimerEnd()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(0);
    }
}
