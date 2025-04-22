/*****************************************************************************
// File Name : MovePoints.cs
// Author : Edward Parra-Garcia
// Creation Date : March 28, 2025
//
// Brief Description : This code creates an array of movepoints for the dev to plug into. 
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoints : MonoBehaviour
{
    [SerializeField] private GameObject[] movePoints;
    [SerializeField] private float speed;
    private int currentIndex;

    /// <summary>
    /// Sets currentIndex to 0 in order to access it and sets as starting point.
    /// </summary>
    void Start()
    {
        currentIndex = 0;
    }

    /// <summary>
    /// Tells the GameObject to move down the array and then reset to the start of the array if the end is reached.
    /// Additionally, tells the GameObject to move towards the currentIndex with its speed value and Time.delaTime.
    /// </summary>
    void Update()
    {
        if (Vector3.Distance(transform.position, movePoints[currentIndex].transform.position) < 0.1f)
        {
            currentIndex++;
            //if current index gets to the end of the array
            if (currentIndex == movePoints.Length)
            {
                //reset the index value
                currentIndex = 0;
            }
        }
        //moves the enemy towards the movepoints using given speed and Time.deltaTime
        transform.position = Vector3.MoveTowards(transform.position, movePoints[currentIndex].transform.position,
            speed * Time.deltaTime);
    }
}
