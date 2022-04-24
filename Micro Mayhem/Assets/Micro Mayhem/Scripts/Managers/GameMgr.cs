// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// FILE NAME: GameMgr.cs
/* FILE DESCRIPTION: Manages attributes associated with the game, such as wave count and loading scenes. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    /*---------- Awake ----------*/
    // Static Instance of GameMgr for global usage
    public static GameMgr inst;
    private void Awake()
    {
        inst = this;
    }

    /*---------- Properties ----------*/
    [Header("Game Attributes")]
    public int waveNumber = 1;


    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If all enemies are dead, go to next wave
        if (AIMgr.inst.enemies.Count == 0)
        {
            waveNumber++;
            // Spawn next wave of enemies
            AIMgr.inst.SpawnEnemies();
        }
    }

    /* EndGame Method
     When the player dies, the game over screen is loaded. */
    public void EndGame()
    {

    }
}
