// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: RunIncrease.cs
/* FILE DESCRIPTION: Allows an object to be consumed by the player upon contact.
 The item will increase the walk speed of the player by 10%. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunIncrease : Consumable
{
    /*---------- Properties ----------*/
    float percentage = 0.1f;

    /*---------- Methods ----------*/
    /* Collect Method
     * Increases run speed of player, destroys gameobject on consumption
     * Removes consumable from active consumables list
     */
    public override void Collect()
    {
        PlayerMgr.inst.runSpeed *= (1 + percentage);

        //if (collectSound)
        //AudioSource.PlayClipAtPoint(collectSound, transform.position
        //collectEffect.Play();

        base.Collect(); // Delete this object
    }
}
