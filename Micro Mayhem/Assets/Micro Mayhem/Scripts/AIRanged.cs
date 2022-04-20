using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRanged : MonoBehaviour
{
    public GameObject Player;
    //PlayerMgr playerMgr;

 
    Vector3 enemyPosition;
    RaycastHit hit;

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
        if (Vector3.Distance(transform.position, Player.transform.position) >= EnemyRangedMgr.inst.MinDistance)
        {
            transform.position += transform.forward * EnemyRangedMgr.inst.Speed * Time.deltaTime;
        }

        //If within mindist range, attack
        if (Vector3.Distance(transform.position, Player.transform.position) <= EnemyRangedMgr.inst.MinDistance)
        {
            RangedAttack();
        }
    }

    void RangedAttack()
    {
        
        AudioMgr.inst.hitTimer -= Time.deltaTime * .9f;

        if (AudioMgr.inst.hitTimer <= 0)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    PlayerMgr.inst.health -= 10;
                    Debug.Log("Ranged Hit");
                    AudioMgr.inst.hitTimer = 1f;
                }
            }
        }

    }
}
