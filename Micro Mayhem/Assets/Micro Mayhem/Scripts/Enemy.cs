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
    public float damage;
    public float health;
    public float speed;


    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    }
}
