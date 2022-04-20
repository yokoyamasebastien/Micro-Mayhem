using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TEMP MELEE AI SCRIPT, MOST WILL BE MOVED TO AIMGR

public class AIMelee : MonoBehaviour
{
    public GameObject Player;
    Vector3 enemyPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        transform.LookAt(Player.transform);
        Debug.DrawRay(transform.position, forward, Color.green);

        //Move enemy if not close enough to player
        if (Vector3.Distance(transform.position, Player.transform.position) >= EnemyMeleeMgr.inst.MinDistance)
        {
            transform.position += transform.forward * EnemyMeleeMgr.inst.Speed * Time.deltaTime;
        }

        //If within mindist range, attack
        if (Vector3.Distance(transform.position, Player.transform.position) <= EnemyMeleeMgr.inst.MinDistance)
        {
            MeleeAttack();
        }

    }

    void MeleeAttack()
    {
        AudioMgr.inst.hitTimer -= Time.deltaTime * .9f;

        if (AudioMgr.inst.hitTimer <= 0)
        {
            PlayerMgr.inst.health -= 20;
            Debug.Log("Melee Hit");
            AudioMgr.inst.hitTimer = 1f;
        }
   
    }
}
