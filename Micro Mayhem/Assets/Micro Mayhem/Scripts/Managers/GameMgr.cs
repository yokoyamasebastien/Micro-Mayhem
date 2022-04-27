// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// FILE NAME: GameMgr.cs
/* FILE DESCRIPTION: Manages attributes associated with the game, such as wave count and loading scenes. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Consumable Assets")]
    public GameObject healthPack;
    public GameObject armorPack;

    public int consumableCount;

    /* Particle Effects */
    [Header("Particle Effects")]
    public ParticleSystem roundEndConfetti;


    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If all enemies are dead, go to next wave
        if(AIMgr.inst.enemyCount == 0)
        {
            RoundEnd();
            AIMgr.inst.SpawnEnemies();
        }
    }

    /*Round End Method
     * Increases wave number
     * shrinks player
     * reduces weapon damage
     * celebration confetti
     * spawns consumables
     * spawns new enemies
     */
    public void RoundEnd()
    {
        SpawnConsumables();
        waveNumber++;
        //PlayerMgr.inst.ShrinkPlayer();
        //PlayerMgr.inst.ReduceWeaponDamage();
        ConfettiSpawn();
    }

    /*
     * Confetti Spawn Method
     * Round End Particle Celebration
     */
    public void ConfettiSpawn()
    {
        roundEndConfetti.Play();
    }

    /* Spawn Health method
     * Spawns health pack worth 50 health
     * Spawns armor pack worth 50 health
     * NOTE: Spawns are currently on the two platforms, but should/could be randomized in the future
     */
    public void SpawnConsumables()
    {
        GameObject healthPackExist = GameObject.FindGameObjectWithTag("Health Pack");
        GameObject armorPackExist = GameObject.FindGameObjectWithTag("Armor Pack");

        /* Create new consumables */
        if (healthPackExist == null) { Instantiate(healthPack, new Vector3(2, 9, 33), Quaternion.identity); }

        if (armorPackExist == null) { Instantiate(armorPack, new Vector3(-39, 9, 14), Quaternion.identity); }
    }


    /* EndGame Method
     When the player dies, the game over screen is loaded. */
    public void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Main Menu");
    }

}
