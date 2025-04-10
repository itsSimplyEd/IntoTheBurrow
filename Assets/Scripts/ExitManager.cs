/*****************************************************************************
// File Name : ExitManager.cs
// Author : Edward Parra-Garcia
// Creation Date : March 14, 2025
//
// Brief Description : Lets the player exit the level if they collide with the exit. Loads next scene with
                       reload delay.
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
    [SerializeField] private MeshRenderer mr;
    [SerializeField] private Collider exitcollider;
    [SerializeField] private bool isFinal;

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (enemyManager.GetEnemyCount() == 0 && !isExitActive)
        {
           ActivateExit();
            isExitActive = true;
        }
    }
    private void OnTriggerEnter(Collider exitEncountered)
    {
        if (exitEncountered.gameObject.tag == "Rue")
        {
            if (isFinal == false)
            {
                Exited();
                //load the next level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                FindObjectOfType<GameManager>().FinalExit();
            }
        }
    }

    void ActivateExit()
    {
        mr.enabled = true;
        GetComponent<Collider>().enabled = true;
    }
    private void Exited()
    {
        hasExited = true;

        Invoke("ReloadScene", reloadDelay);
    }
}
