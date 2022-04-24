// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// FILE NAME: Enemy.cs
/* FILE DESCRIPTION: Contains an abstract Enemy class to inherit from, for different types of enemies. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    /*---------- Properties ----------*/
    [Header("Physics-Related Properties")]
    public Transform enemyBody;
    public CapsuleCollider enemyCollider;

    public GameObject player;

    [Header("Attributes")]
    public bool isDead = false;

    public int health;
    public float speed;
    public int listIndex;

    [Header("Attack Attributes")]
    public int damage;
    public float maxDistance;   // Max Distance that enemy can attack player from

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    public void Start()
    {
        enemyBody = gameObject.GetComponent<Transform>();
        enemyCollider = gameObject.GetComponent<CapsuleCollider>();

        player = PlayerMgr.inst.player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Move Method
     Moves the enemy towards the player. */
    public virtual void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    /* Attack Method
     Used to attack the player */
    public virtual void Attack()
    {

    }

    /* TakeDamage Method
     Used when enemy entity takes damage. */
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        // If health is <= 0, the enemy dies
        if(health <= 0) { Die(); }
    }

    /* Die Method
     Used when the enemy entity's health is <= 0. */
    void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
