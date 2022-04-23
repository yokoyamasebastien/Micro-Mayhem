// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: EnemyMelee.cs
/* FILE DESCRIPTION: Contains the code that controls a melee enemy. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        transform.LookAt(player.transform);
        Debug.DrawRay(transform.position, forward, Color.green);

        //Move enemy if not close enough to player
        if (Vector3.Distance(transform.position, player.transform.position) >= maxDistance)
        {
            Move();
        }

        //If within mindist range, attack
        if (Vector3.Distance(transform.position, player.transform.position) <= maxDistance)
        {
            Attack();
        }
    }



    /* Attack Method
    Overrides/Hides Base Enemy Attack Method
    Used to attack the player */
    public new void Attack()
    {
        AudioMgr.inst.hitTimer -= Time.deltaTime * .9f;

        if (AudioMgr.inst.hitTimer <= 0)
        {
            PlayerMgr.inst.TakeDamage(damage);
            Debug.Log("Melee Hit");
            AudioMgr.inst.hitTimer = 1f;
        }
    }
}
