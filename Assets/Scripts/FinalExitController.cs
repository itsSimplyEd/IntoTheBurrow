
/*****************************************************************************
// File Name : FollowPlayer.cs
// Author : Edward Parra-Garcia
// Creation Date : March 30, 2025
//
// Brief Description : This code allows the player to return to the main menu when colliding with the final exit.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinalExitController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rue")
        {
            FindObjectOfType<GameManager>().FinalExit();
        }
    }
}
