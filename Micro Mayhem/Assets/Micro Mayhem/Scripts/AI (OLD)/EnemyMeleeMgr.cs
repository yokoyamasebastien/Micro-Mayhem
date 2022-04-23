using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeMgr : MonoBehaviour
{
    /*---------- Awake ----------*/
    // Static Instance of EnemyMeleeMgr for global usage
    public static EnemyMeleeMgr inst;
    private void Awake()
    {
        inst = this;
    }

    /*---------- Properties ----------*/
    [Header("Physics-Related Properties")]
    public Transform enemyBody;
    public CapsuleCollider enemyCollider;

    [Header("Movement")]
    public float Speed = 5f;
    public float MinDistance = 1f;

    public float distToGround = 1f; // Distance from center of player capsule to bottom of capsule, where ground makes contact

    public Vector3 velocity;

    [Header("Enemy Attributes")]
    public int health;

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
        health = 150;
    }

    // Update is called once per frame
    void Update()
    {
        //display enemy health

        //if enemy health is 0
        //deactivate entity
    }
}
