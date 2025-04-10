/*****************************************************************************
// File Name : EnemyController.cs
// Author : Edward Parra-Garcia
// Creation Date : March 31, 2025
//
// Brief Description : If enemy collides with Rue(player) then it will destroy the gameObject.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// Deletes the enemy if player (Rue) collides with the collider of the hitbox.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        bool isSelf = false;
        foreach (var c in collision.contacts)
        {
            // If collision was with collider on gameobject with this script.
            if (GetComponent<Collider>() == c.thisCollider)
                isSelf = true;
        }
        // Asks if collision was with tag "Rue" and if so, destroys self.
       if (collision.gameObject.tag == "Rue")
        {
            if (isSelf)
            {
                collision.gameObject.GetComponent<RueLife>().Die();
            }
            else
            {
                FindObjectOfType<EnemyManager>().RemoveEnemy(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
