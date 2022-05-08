// AUTHOR: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: PlayerMgr.cs
/* FILE DESCRIPTION: Manages attributes and properties that are associated with the player. */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    /*---------- Awake ----------*/
    // Static Instance of PlayerMgr for global usage
    public static PlayerMgr inst;
    private void Awake()
    {
        inst = this;
    }

    /*---------- Properties ----------*/
    [Header("Physics-Related Properties")]
    public GameObject player;
    public Transform playerBody;
    public CharacterController characterController;
    public CapsuleCollider playerCollider;

    [Header("Movement")]
    public float walkSpeed = 12f;
    public float runSpeed = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float distToGround = 1f; // Distance from center of player capsule to bottom of capsule, where ground makes contact

    public Vector3 velocity;
    
    float shrinkDelta = (float)0.06;
    float scaleValue = 1;

    [Header("Player Attributes")]
    public int health;
    public int armor;

    /* We must put a cap on the player's health and armor
     * Some powerups can increase the cap. */
    private int maxHealth;
    private int maxArmor;

    [Header("Player Inventory")]
    public GameObject weapon;
    public Gun gun;
    public int currentGunID = 0;
    public bool isScoped = false;

    [Header("Cameras")]
    public GameObject weaponCamera;
    public Camera playerCamera;
    public float normalFOV;

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
        //health = 100;
        //armor = 0;

        SelectWeapon();

        maxHealth = 100;
        maxArmor = 100;

        normalFOV = playerCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {

        /*----- Y-axis Movement -----*/
        // Check if you land on the ground
        if(Physics.Raycast(playerBody.position, Vector3.down, distToGround + 0.1f) && velocity.y < 0)
        {
            velocity.y = 0;
        }

        // deltaY = gravity * (time ^ 2)
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        // Check if health is 0, then game over.
        if (PlayerMgr.inst.health <= 0)
        {
            // GameMgr EndGame Method to switch back to main menu
            GameMgr.inst.EndGame();
        }


        /*----- Take Damage Audio Timer -----*/
        AudioMgr.inst.hitTimer -= Time.deltaTime * 0.2f;
    }

    /* TakeDamage Method
    Expects an int
    Called when a player is supposed to receive damage.
    The damage amount is subtracted from the player's health. */
    public void TakeDamage(int damage)
    {
        AudioMgr.inst.PlayDamageSound();

        // Calculate new armor amount, based on damage
        int newArmor = Math.Max(0, (armor - ((2 * damage) / 5)));

        // Calculate new damage amount based on damage value
        int deltaHealth;

        // If armor was 0, or armor goes down to 0 from initial value
        if (newArmor == 0)
        {
            deltaHealth = damage - (2 * armor);
        }
        // If player still has armor
        else
        {
            deltaHealth = damage / 4;
        }
        
        armor = newArmor;
        health -= deltaHealth;
    }

    /* Heal Method
    Expects an int
    Called when a player is supposed to heal
    The heal amount is added to the player's health. */
    public void Heal(int heal)
    {
        health += heal;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    /* AddArmor Method
    Expects an int
    Called when a player gains armor
    The armor amount is added to the player's armor. */
    public void AddArmor(int armorAmount)
    {
        armor += armorAmount;

        if(armor >= maxArmor)
        {
            armor = maxArmor;
        }
    }

    /* ShrinkPlayer Method
     * Lowers the players position by .5 after every round.
     * Shrinks the player by a delta of .25 after every round.
     * NOTE: After wave 3, gun dissapears due to it not shrinking with player. Will need to do the math on scaling the gun with the player.
     */
    public void ShrinkPlayer()
    {
        //Move player down in position so he is not floating/clipping through objects
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - (shrinkDelta * 2), player.transform.position.z);
        //Shrink player
        scaleValue -= shrinkDelta;
        player.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }

    /*ReduceWeaponDamage Method
     * Reduces damage of all weapons by 25%
     */
    public void ReduceWeaponDamage()
    {
        foreach(Transform weaponObject in weapon.transform)
        {
            float newDamage = weaponObject.GetComponent<Gun>().damage * 0.75f;
            weaponObject.GetComponent<Gun>().damage = (int)newDamage;
        }
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weaponObject in weapon.transform)
        {
            if(i == currentGunID)
            {
                weaponObject.gameObject.SetActive(true);
                gun = weaponObject.GetComponent<Gun>();
            }
            else
            {
                weaponObject.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
