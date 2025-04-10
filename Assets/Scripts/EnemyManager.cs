/*****************************************************************************
// File Name : EnemyManager.cs
// Author : Edward Parra-Garcia
// Creation Date : March 28, 2025
//
// Brief Description : This code makes a list of enemies for the dev to add enemies into. Once an enemy is killed,
                       this script will remove an enmemy from the list.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    /// <summary>
    /// Adds enemy from the list 
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
    /// <summary>
    /// Removes enemy from the list
    /// </summary>
    /// <param name="enemy"></param>
    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
    /// <summary>
    /// Checks if list still has enemies
    /// </summary>
    /// <returns></returns>
    public int GetEnemyCount()
    {
        return enemies.Count;
    }

    /// <summary>
    /// Destroys this script when player dies.
    /// </summary>
    private void OnDestroy()
    {
        RemoveEnemy(gameObject);
    }

}
