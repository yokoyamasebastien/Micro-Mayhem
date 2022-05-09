// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: DamageIncrease.cs
/* FILE DESCRIPTION: Allows an object to be consumed by the player upon contact.
 The item will increase the damage of all of the player's weapons by 15%. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIncrease : Consumable
{
    /*---------- Properties ----------*/
    float percentage = 0.15f;

    /*---------- Methods ----------*/
    /* Collect Method
     * Increases damage of player weapons, destroys gameobject on consumption
     * Removes consumable from active consumables list
     */
    public override void Collect()
    {
        foreach (Transform weaponObject in PlayerMgr.inst.weapon.transform)
        {
            Gun weapon = weaponObject.GetComponent<Gun>();
            float fDamage = weapon.damage * (1 + percentage);
            weapon.damage = (int)fDamage;
        }

        AudioSource.PlayClipAtPoint(collectSound, transform.position);  // Play sound

        base.Collect(); // Delete this object
    }
}
