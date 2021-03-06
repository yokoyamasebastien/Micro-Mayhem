// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: JumpIncrease.cs
/* FILE DESCRIPTION: Allows an object to be consumed by the player upon contact.
 The item will increase the jump height of the player by 15%. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpIncrease : Consumable
{
    /*---------- Properties ----------*/
    float percentage = 0.15f;

    /*---------- Methods ----------*/
    /* Collect Method
     * Increases jump height of player, destroys gameobject on consumption
     * Removes consumable from active consumables list
     */
    public override void Collect()
    {
        PlayerMgr.inst.jumpHeight *= (1 + percentage);

        AudioSource.PlayClipAtPoint(collectSound, transform.position);  // Play sound

        base.Collect(); // Delete this object
    }
}
