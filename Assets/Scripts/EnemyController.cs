/*****************************************************************************
// File Name : EnemyController.cs
// Author : Edward Parra-Garcia
// Creation Date : March 31, 2025
//
// Brief Description : If enemy collides with Rue(player) then it will destroy the gameObject. If Rue(player) hits the
                       enemy(not the hitbox), theu will get function Die() and get deleted.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private AudioSource rueBackstab;
    [SerializeField] private AudioSource enemyAttacks;
    /// <summary>
    /// Deletes the enemy if player (Rue) collides with the collider of the hitbox and gets function Die() from 
    /// RueLife script and will delete the player.
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
        //Asks if collision was with tag "Rue" and if so, destroys self. Plays sound for being killed and killing the
        //player.
       if (collision.gameObject.tag == "Rue")
        {
            if (isSelf)
            {
                enemyAttacks.Play();
                collision.gameObject.GetComponent<RueLife>().Die();
            }
            else
            {
                rueBackstab.Play();
                FindObjectOfType<EnemyManager>().RemoveEnemy(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
