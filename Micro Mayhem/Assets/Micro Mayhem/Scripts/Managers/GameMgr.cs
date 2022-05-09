// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// FILE NAME: GameMgr.cs
/* FILE DESCRIPTION: Manages attributes associated with the game, such as wave count and loading scenes. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

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
    public int waveNumber;

    [Header("Consumable Assets")]
    public GameObject healthPack;
    public GameObject armorPack;

    public List<GameObject> upgrades;
    public List<GameObject> weapons;
    int weaponsIndex = 0;

    public int consumableCount;

    /* Particle Effects */
    [Header("Particle Effects")]
    public ParticleSystem roundEndConfetti;

    [Header("Objects to Instantiate")]
    public Rigidbody rocket;

    public GameObject rocketSpawnPoint;


    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
        AIMgr.inst.SpawnEnemies();
        waveNumber = 1;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // If all enemies are dead, go to next wave
        if (AIMgr.inst.enemyCount == 0)
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
        if(waveNumber % 2 == 0)
        {
            SpawnWeapon();
        }

        SpawnConsumables();
        SpawnUpgrade();
        waveNumber++;

        if (PlayerMgr.inst.scaleValue >= 0.20) {
            PlayerMgr.inst.ShrinkPlayer();
        }

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

    public void SpawnUpgrade()
    {
        int index = Random.Range(0, (upgrades.Count - 1));
        GameObject upgrade = upgrades[index];

        float randX = Random.Range(PlayerMgr.inst.player.transform.position.x - 10f, PlayerMgr.inst.player.transform.position.x + 10f);
        float randZ = Random.Range(PlayerMgr.inst.player.transform.position.z - 10f, PlayerMgr.inst.player.transform.position.z + 10f);
        Vector3 location = new Vector3(randX, 1f, randZ);

        Instantiate(upgrade, location, Quaternion.identity);
    }

    public void SpawnWeapon()
    {
        GameObject weapon = weapons[weaponsIndex];

        Instantiate(weapon, new Vector3(0, 1, 0), Quaternion.identity);
        weaponsIndex++;
    }

    /* EndGame Method
     When the player dies, the game over screen is loaded. */
    public void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Main Menu");
    }
}
