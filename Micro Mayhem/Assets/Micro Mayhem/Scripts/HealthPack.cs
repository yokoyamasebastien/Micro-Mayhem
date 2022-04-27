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
