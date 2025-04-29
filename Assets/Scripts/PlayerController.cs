/*****************************************************************************
// File Name : PlayerController.cs
// Author : Edward Parra-Garcia
// Creation Date : February 21, 2025
//
// Brief Description : This code allows the player to move, burrow, restart the game, and quit
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
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject rue;
    [SerializeField] private GameObject burrowModel;
    [SerializeField] private MeshRenderer rueRenderer;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction burrow;
    [SerializeField] private InputAction restart;
    [SerializeField] private InputAction quit;
    [SerializeField] private InputAction pause;
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private Image timer;
    [SerializeField] private bool isRueBurrowed;
    [SerializeField] private bool timerOn;
    [SerializeField] private float burrowTimer;
    [SerializeField] private float burrowTimerMax = 2f;
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

    /// <summary>
    /// Gets the function RueBurrows() which allows the player to be in the "Burrowed" state
    /// </summary>
    /// <param name="context"></param>
    private void Handle_burrowstarted(InputAction.CallbackContext context)
    {
        RueBurrows();
    }

    /// <summary>
    /// Finds MainMenu and resumes the game!
    /// </summary>
    /// <param name="context"></param>
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

    /// <summary>
    /// Turns off physics, turns off movement, and changes the player's layer to "Burrowed." Additonally, starts the 
    /// coroutine to reenable the player.
    /// </summary>
    private void RueBurrows()
    {
        //turn off physics for the player (gravity)
        GetComponent<Rigidbody>().isKinematic = false;
        //turns off player movement
        GetComponent<PlayerController>().enabled = false;
        //Changes the player's layer to "Burrowed"
        gameObject.layer = LayerMask.NameToLayer("Burrowed");
        //Reenables Rue's main actions
        StartCoroutine(ReenableRue());
    }

    /// <summary>
    /// Sets the timer image to active and sets the fill amount to full. The bool is switched to true and the timer 
    /// ticks down by Time.deltaTime. The timer counts down gradually (burrowTimer/burrowTimerMax) till it reaches 0.
    /// Then after the timer reaches 0, the player is reenabled, the player's layer is changed to 
    /// "Default", burrowTimer will equal burrowTimerMax, and timer image will be set to false.
    /// </summary>
    /// <returns></returns>
    IEnumerator ReenableRue()
    {
        //The bool is switched to true
        timer.gameObject.SetActive(true);
        timer.fillAmount = 1f;
        burrowModel.SetActive(true);
        while (burrowTimer >  0)
        {
            timerOn = true;
            burrowTimer -= Time.deltaTime;
         //The timer counts down gradually(burrowTimer / burrowTimerMax) till it reaches 0
            timer.fillAmount = burrowTimer/burrowTimerMax;
            yield return null;
        }
        gameObject.SetActive(false);
        //the player is reenabled
        GetComponent <PlayerController>().enabled = true;
        //the player's layer is changed to "Default"
        //burrowTimer will equal burrowTimerMax
        gameObject.layer = LayerMask.NameToLayer("Default");
        burrowTimer = burrowTimerMax;
        timer.fillAmount = 0;
        //timer image will be set to false
        timer.gameObject.SetActive (false);
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
