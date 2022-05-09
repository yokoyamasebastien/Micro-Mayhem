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
    public int enemyCount;

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* SpawnEnemies Method
     Spawns enemies in a random area near the player. */
    public void SpawnEnemies()
    {
        float minDist = 0; // Minimum distance enemies must spawn from player
        float maxDist = 40;    // Maximum distance enemies can spawn from player

        meleePrefab.health *= (int)(1 + (0.1f * GameMgr.inst.waveNumber));
        rangedPrefab.health *= (int)(1 + (0.1f * GameMgr.inst.waveNumber));

        for (int i = 0; i < 5; i++) //melee
        {
            //instantiate object in random range
            var spawnPos = new Vector3(Random.Range(minDist, maxDist), 1, Random.Range(minDist, maxDist));
            Instantiate(meleePrefab, spawnPos, Quaternion.identity);
        }

        for (int i = 0; i < 5; i++) //ranged
        {
            //instantiate object in random range
            var spawnPos = new Vector3(Random.Range(minDist, maxDist), 1, Random.Range(minDist, maxDist));
            Instantiate(rangedPrefab, spawnPos, Quaternion.identity);
        }

    }
}
