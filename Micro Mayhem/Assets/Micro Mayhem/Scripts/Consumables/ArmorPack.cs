// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: ArmorPack.cs
/* FILE DESCRIPTION: Allows an object to be consumed by the player upon contact.
 * The item will increase the player's armor by 50. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPack : Consumable
{
    /*---------- Properties ----------*/
    int armor = 50;

    /*---------- Methods ----------*/
    /* Collect Method
     * Adds armor to player, destroys gameobject on consumption
     * Removes consumable from active consumables list
     */
    public override void Collect()
    {
        PlayerMgr.inst.armor += armor;

        //if (collectSound)
        //AudioSource.PlayClipAtPoint(collectSound, transform.position
        //collectEffect.Play();

        base.Collect(); // Delete this object
    }
}
