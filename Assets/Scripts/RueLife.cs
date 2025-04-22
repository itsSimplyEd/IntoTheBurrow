/*****************************************************************************
// File Name : RueLife.cs
// Author : Edward Parra-Garcia
// Creation Date : February 28, 2025
//
// Brief Description : This code allows the player to disappear, stop movement, and die through collision or if Rue 
                       has a Y value of -9. Additonally has the script to load the current scence when restarted.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RueLife : MonoBehaviour
{
    private bool isDead = false;
    [SerializeField] private float reloadDelay = 1.5f;
    [SerializeField] private int dyingYValue = -9;

    /// <summary>
    /// Makes player die and invokes Reload Scene().
    /// </summary>
    public void Die()
    {
        //make player disappear
        GetComponent<MeshRenderer>().enabled = false;

        //stop player movement
        GetComponent<PlayerController>().enabled = false;

        //turn off physics for the player (gravity)
        GetComponent<Rigidbody>().isKinematic = false;
        //makes player die 
        isDead = true;

        Invoke("ReloadScene", reloadDelay);
    }
    /// <summary>
    /// Loads current scence through SceneManager.
    /// </summary>
    void ReloadScene()
    {
        //loads current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Checks if the player is dead, if not, kill them
    /// </summary>
    private void Update()
    {
        if (transform.position.y < dyingYValue && !isDead)
        {
            //DIE!
            Die();
        }
    }
}
