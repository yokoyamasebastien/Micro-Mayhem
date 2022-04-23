// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: AIMgr.cs
/* FILE DESCRIPTION: Manages all the AI in the game. In this case, all of the enemies. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMgr : MonoBehaviour
{
    /*---------- Awake ----------*/
    // Static Instance of AIMgr for global usage
    public static AIMgr inst;
    private void Awake()
    {
        inst = this;
    }


    /*---------- Properties ----------*/
    public List<Enemy> enemies;


    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if any enemies died and remove them
        enemies.Remove(enemies.Find(e => e.isDead == true));
    }

    /* SpawnEnemies Method
     Spawns enemies in a random area near the player. */
    void SpawnEnemies()
    {
        float minDist = 30; // Minimum distance enemies must spawn from player
        float maxDist = 100;    // Maximum distance enemies can spawn from player


    }
}
