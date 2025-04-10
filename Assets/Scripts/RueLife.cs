/*****************************************************************************
// File Name : RueLife.cs
// Author : Edward Parra-Garcia
// Creation Date : February 28, 2025
//
// Brief Description : This code makes Rue (the player) die if they collide with an enemy or have a Y value of -9.
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
    /// Deletes the palyers mesh, turns off physics, and stops playr movement. (AKA DEATH)
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.tag == "Enemy")
        {
            //Debug.Log(collision.gameObject.name);
            //make player disappear
            //GetComponent<MeshRenderer>().enabled = false;

            //stop player movement
           // GetComponent<PlayerController>().enabled = false;

            //turn off physics for the player (gravity)
            //GetComponent<Rigidbody>().isKinematic = false;

            //DIE
           // Die();
        }
    }

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
