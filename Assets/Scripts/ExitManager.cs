/*****************************************************************************
// File Name : ExitManager.cs
// Author : Edward Parra-Garcia
// Creation Date : March 14, 2025
//
// Brief Description : Lets the player exit the level if they collide with the exit. Loads next scene with
                       reload delay. Additionally, activates the exit when EnemyCount is 0 and isExitActive is false.
                       This code also checks if an exit is marked as final exit and will either load the next scene
                       or return the player to the MainMenu. This will activate the meshRenderer and Collider for 
                       the exit. Also plays sound for when a level is completed and when a player enters the exit.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    [SerializeField] private GameObject exit;
    private bool hasExited = false;
    private bool isExitActive = false;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private float reloadDelay = 1.5f;
    [SerializeField] private MeshRenderer mR;
    [SerializeField] private Collider exitCollider;
    [SerializeField] private bool isFinal;
    [SerializeField] private AudioSource victorySound;
    [SerializeField] private AudioSource burrowSound;

    /// <summary>
    /// checks if the enemy count in the array is equal to 0 and if isExitActive is false, if so, gets ActivateExit() 
    /// and sets isExitAcrice to true. Plays victory sound as well.
    /// </summary>
    private void Update()
    {
        if (enemyManager.GetEnemyCount() == 0 && !isExitActive)
        {
           ActivateExit();
           isExitActive = true;
        }
    }

    /// <summary>
    /// When a player passes through this trigger, if the isFinal bool is false, they will load the next active scene.
    /// If the isFinal bool is true, they will get the FinalExit function (Load MainMenu) and disable player movement.
    /// </summary>
    /// <param name="exitEncountered"></param>
    private void OnTriggerEnter(Collider exitEncountered)
    {
        if (exitEncountered.gameObject.tag == "Rue")
        {
            if (isFinal == false)
            {
                burrowSound.Play();
                Exited();
                //load the next level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                burrowSound.Play();
                //loads the MainMenu
                FindObjectOfType<GameManager>().FinalExit();
                FindObjectOfType<PlayerController>().enabled = false;
            }
        }
    }

    /// <summary>
    /// Activates the meshRenderer and the Collider for the exit. Plays sound when player exits the level.
    /// </summary>
    void ActivateExit()
    {
        victorySound.Play();
        mR.enabled = true;
        GetComponent<Collider>().enabled = true;
    }

    /// <summary>
    /// If hasExited bool is true, will load the next scene
    /// </summary>
    private void Exited()
    {
        hasExited = true;

        Invoke("ReloadScene", reloadDelay);
    }
}
