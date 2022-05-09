// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: FireRateIncrease.cs
/* FILE DESCRIPTION: Allows an object to be consumed by the player upon contact.
 The item will increase the fire rate of all of the player's weapons by 15%. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateIncrease : Consumable
{
    /*---------- Properties ----------*/
    float percentage = 0.15f;

    /*---------- Methods ----------*/
    /* Collect Method
     * Increases fire rate of player weapons, destroys gameobject on consumption
     * Removes consumable from active consumables list
     */
    public override void Collect()
    {
        //PlayerMgr.inst.armor += armor;

        foreach (Transform weaponObject in PlayerMgr.inst.weapon.transform)
        {
            Gun weapon = weaponObject.GetComponent<Gun>();
            weapon.timeBetweenShots *= (1 - percentage);
            weapon.timeBetweenShooting *= (1 - percentage);
        }

        AudioSource.PlayClipAtPoint(collectSound, transform.position);  // Play sound

        base.Collect(); // Delete this object
    }
}
