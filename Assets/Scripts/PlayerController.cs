/*****************************************************************************
// File Name : FollowPlayer.cs
// Author : Edward Parra-Garcia
// Creation Date : February 21, 2025
//
// Brief Description : This code allows the player to move,jump (jumping was removed), restart the game, and quit
                       through the new input system. Allows the dev to access the speed value and destroys an enemy if
                       the player collides with its hitbox. Allows player to win and return to main menu if they hit 
                       the final exit.
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject rue;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction burrow;
    [SerializeField] private InputAction restart;
    [SerializeField] private InputAction quit;
    [SerializeField] private InputAction pause;
    [SerializeField] private float jumpValue = 5f;
    [SerializeField] private float playerSpeed = 10f;
    private Rigidbody rb;
    private Vector3 playerMovement;
    /// <summary>
    /// Tells the game to grab the Rigidbody and apply it to the player and allows the player to access the input map.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput.currentActionMap.Enable();
        restart = playerInput.currentActionMap.FindAction("Restart");
        quit = playerInput.currentActionMap.FindAction("Quit");
        pause = playerInput.currentActionMap.FindAction("Pause");
        burrow = playerInput.currentActionMap.FindAction("Burrow");
        pause.started += Handle_pausestarted;
        restart.started += Handle_restartstarted;
        quit.started += Handle_quitstarted;
        burrow.started += Handle_burrowstarted;
        
    }

    private void Handle_burrowstarted(InputAction.CallbackContext context)
    {
       
    }

    private void Handle_pausestarted(InputAction.CallbackContext context)
    {
        FindObjectOfType<MainMenu>().ResumeGame();
    }

    /// <summary>
    /// Allows players to quit the game!
    /// </summary>
    /// <param name="context"></param>
    private void Handle_quitstarted(InputAction.CallbackContext context)
    {
        Application.Quit();
        Debug.Log("YOU QUIT");
    }

    /// <summary>
    /// Allows players to reset the CURRENT scene for the game!
    /// </summary>
    /// <param name="context"></param>
    private void Handle_restartstarted(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Allows the player to jump and lets the dev set the value for jump.
    /// </summary>
    void OnJump()
    {
        rb.velocity = new Vector3(0, jumpValue, 0);
    }

    /// <summary>
    /// Allows the player to move and gives the dev flexibility to change speed values.
    /// </summary>
    /// <param name="iValue"></param>
    private void OnMove(InputValue iValue)
    {
        Vector2 inputMovement = iValue.Get<Vector2>();
        playerMovement.x = inputMovement.x * playerSpeed;
        playerMovement.z = inputMovement.y * playerSpeed;
    }
    /// <summary>
    /// Moves the player
    /// </summary>
    void Update()
    {
        rb.velocity = new Vector3(playerMovement.x, rb.velocity.y, playerMovement.z);
    }
    /// <summary>
    /// Allows the player to destroy the enemy(parent) if the hitbox is hit.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        //if the player collides with the enemy's hitbox it destroys the enemy and grabs FinalExit() and executes if
        //the player collides with the FinalExit.
        if (collision.gameObject.tag == "FinalExit")
        {
            FindObjectOfType<GameManager>().FinalExit();
        }
    }

    private void RueBurrows()
    {
       
    }

    /// <summary>
    /// Destroys the input maps if the playerController script is destroyed.
    /// </summary>
    private void OnDestroy()
    {
        restart.started -= Handle_restartstarted;
        quit.started -= Handle_quitstarted;
        pause.started -= Handle_pausestarted;
        burrow.started -= Handle_burrowstarted;
    }
}
