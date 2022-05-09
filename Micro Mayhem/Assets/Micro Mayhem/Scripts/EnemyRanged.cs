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
    bool shouldRun = false;
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
        /*
        if (Vector3.Distance(transform.position, player.transform.position) > maxDistance)
        {
            shouldRun = true;
        }

        // If within mindist range, attack
        else if (Vector3.Distance(transform.position, player.transform.position) <= maxDistance)
        {
            shouldRun = false;
            Attack();
        }
        */
        if (Vector3.Distance(enemyRB.position, player.transform.position) <= maxDistance)
        {
            shouldRun = false;
            Attack();
        }
        else
            shouldRun = true;
    }

    void FixedUpdate()
    {
        if (enemyBody.position.y <= 0)
        {
            TakeDamage(1000);
        }

        //Ranged unit constantly adjusts to look at player
        angle = Mathf.Atan2(player.transform.position.x, player.transform.position.z) * Mathf.Rad2Deg;
        eulerAngleVelocity = new Vector3(0, angle, 0);
        deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        enemyRB.MoveRotation(enemyRB.rotation * deltaRotation);
        Debug.DrawLine(enemyRB.position, Vector3.forward, Color.yellow);

        //If within attack range, attack
        if (shouldRun)
            Move();
    }

    /* Attack Method
    Overrides/Hides Base Enemy Attack Method
    Used to attack the player */
    public new void Attack()
    {
        AudioMgr.inst.hitTimer -= Time.deltaTime * .3f;

        if (AudioMgr.inst.hitTimer <= 0)
        {
            if (Physics.Raycast(enemyRB.position, transform.TransformDirection(Vector3.forward), out hit))
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

    /* Move Method
     * Moves rigid body towards player location
     * calculates angle
     */
    public new void Move()
    {
        angle = Mathf.Atan2(player.transform.position.x, player.transform.position.z) * Mathf.Rad2Deg;
        eulerAngleVelocity = new Vector3(0, angle, 0);
        deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        enemyRB.MoveRotation(enemyRB.rotation * deltaRotation);

        Vector3 direction = (player.transform.position - transform.position).normalized;
        enemyRB.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }
}
