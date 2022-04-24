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

    /*------------Prefabs-----------*/
    public Enemy meleePrefab;
    public Enemy rangedPrefab;


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
    public void SpawnEnemies()
    {
        float minDist = 30; // Minimum distance enemies must spawn from player
        float maxDist = 100;    // Maximum distance enemies can spawn from player

        for (int i = 0; i < 5; i++) //melee
        {
            //instantiate object in random range
            var spawnPos = new Vector3(Random.Range(minDist, maxDist), 1, Random.Range(minDist, maxDist));
            enemies.Add(Instantiate(meleePrefab, spawnPos, Quaternion.identity));
        }

        for (int i = 0; i < 5; i++) //ranged
        {
            //instantiate object in random range
            var spawnPos = new Vector3(Random.Range(minDist, maxDist), 1, Random.Range(minDist, maxDist));
            enemies.Add(Instantiate(rangedPrefab, spawnPos, Quaternion.identity));
        }

    }
}
