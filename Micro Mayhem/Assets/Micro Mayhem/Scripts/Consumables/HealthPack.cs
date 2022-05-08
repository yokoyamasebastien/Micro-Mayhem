// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: HealthPack.cs
/* FILE DESCRIPTION: Allows an object to be consumed by the player upon contact.
 * The item will heal the player's health by 50. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Consumable
{
    /*---------- Properties ----------*/
    int health = 50;

    /*---------- Methods ----------*/
    /* Collect Method
     * Adds armor to player, destroys gameobject on consumption
     * Removes consumable from active consumables list
     */
    public override void Collect()
	{
        PlayerMgr.inst.health += health;

        //if (collectSound)
        //AudioSource.PlayClipAtPoint(collectSound, transform.position
        //collectEffect.Play();

        base.Collect(); // Delete this object
    }
}
