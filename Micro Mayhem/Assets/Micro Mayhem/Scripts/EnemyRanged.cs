// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: EnemyRanged.cs
/* FILE DESCRIPTION: Contains the code that controls a ranged-attack enemy. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    /*---------- Properties ----------*/
    [Header("Attack-Related Attributes")]
    RaycastHit hit;


    /*---------- Methods ----------*/
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();   // Call Start Method of base class
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        transform.LookAt(player.transform);
        Debug.DrawRay(transform.position, forward, Color.green);

        // Move enemy if not close enough to player
        if (Vector3.Distance(transform.position, player.transform.position) >= maxDistance)
        {
            Move();
        }

        // If within mindist range, attack
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    PlayerMgr.inst.TakeDamage(damage);
                    Debug.Log("Ranged Hit");
                    AudioMgr.inst.hitTimer = 1f;
                }
            }
        }
    }
}
