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
    public float minDistance = 2f;

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    /*void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;

        //If within mindist range, attack
        if (Vector3.Distance(transform.position, player.transform.position) <= maxDistance)
        {
            Attack();
        }
    }
    */

    void FixedUpdate()
    {

        if (enemyBody.position.y <= 0)
        {
            TakeDamage(1000);
        }

        //Constantly move melee enemy towards player
        //Move();

        if (Vector3.Distance(transform.position, player.transform.position) > minDistance)
        {
            Move();
        }

        //If within mindist range, attack
        if ((Vector3.Distance(transform.position, player.transform.position) <= maxDistance) && 
            (Vector3.Distance(transform.position, player.transform.position) >= (minDistance * 0.95f)))
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
            AudioMgr.inst.PlayMeleeFire(gameObject);
            PlayerMgr.inst.TakeDamage(damage);
            Debug.Log("Melee Hit");
            AudioMgr.inst.hitTimer = 1f;
        }
    }

    public new void Move()
    {
            angle = Mathf.Atan2(player.transform.position.x, player.transform.position.z) * Mathf.Rad2Deg;
            eulerAngleVelocity = new Vector3(0, angle, 0);
            deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
            enemyRB.MoveRotation(enemyRB.rotation * deltaRotation);
            Vector3 direction = (player.transform.position - transform.position).normalized;
            enemyRB.MovePosition(transform.position + direction * speed * Time.deltaTime);
            Debug.DrawLine(transform.position, Vector3.forward, Color.black);
        
    }
}
