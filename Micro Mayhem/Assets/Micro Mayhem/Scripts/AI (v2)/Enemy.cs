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
    public float rayRange = 1.2f;
    public float angle;
    public Vector3 eulerAngleVelocity;
    public Quaternion deltaRotation;

    /*---------- Properties ----------*/
    [Header("Physics-Related Properties")]
    public Transform enemyBody;
    public CapsuleCollider enemyCollider;
    public Rigidbody enemyRB;
    public GameObject player;

    [Header("Attributes")]
    public int health;
    public float speed;
    public int listIndex;

    [Header("Attack Attributes")]
    public int damage;
    public float maxDistance;
    //public float minDistance = 1f; // Max Distance that enemy can attack player from

    [Header("Enemy Audio")]
    public AudioClip enemyDeath;

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    public void Start()
    {
        enemyBody = gameObject.GetComponent<Transform>();
        enemyCollider = gameObject.GetComponent<CapsuleCollider>();
        enemyRB = gameObject.GetComponent<Rigidbody>();
        //enemySource = gameObject.GetComponent<>
        enemyRB.freezeRotation = true;

        player = PlayerMgr.inst.player;

        AIMgr.inst.enemyCount++;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
    }

    /* Move Method
     Moves the enemy towards the player. */
    public virtual void Move()
    {
        
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
        if(health <= 0) 
        {
            Die();
        }
    }

    /* Die Method
     Used when the enemy entity's health is <= 0. */
    void Die()
    {
        PlayEnemyDeath();
        AIMgr.inst.enemyCount--;
        Destroy(gameObject);
    }

    public void PlayEnemyDeath()
    {
        AudioSource.PlayClipAtPoint(enemyDeath, transform.position, 5f);
    }
}
