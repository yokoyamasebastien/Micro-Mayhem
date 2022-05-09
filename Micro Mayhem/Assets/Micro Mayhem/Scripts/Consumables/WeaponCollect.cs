// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: WeaponCollect.cs
/* FILE DESCRIPTION: Allows an object to be consumed by the player upon contact.
 The item will add the weapon to the player's inventory. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollect : Consumable
{
    /*---------- Properties ----------*/
    public GameObject prefab;
    public Vector3 position;

    /*---------- Methods ----------*/
    /* Collect Method
     * Increases damage of player weapons, destroys gameobject on consumption
     * Removes consumable from active consumables list
     */
    public override void Collect()
    {
        GameObject gun = Instantiate(prefab);
        gun.transform.parent = PlayerMgr.inst.weapon.transform;
        gun.transform.localPosition = position;
        gun.transform.localRotation = Quaternion.identity;
        gun.SetActive(false);
        PlayerMgr.inst.weaponList.Add(gun);

        AudioSource.PlayClipAtPoint(collectSound, transform.position);  // Play sound

        base.Collect(); // Delete this object
    }
}
