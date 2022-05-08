// NAME: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: Consumable.cs
/* FILE DESCRIPTION: Allows an object to be consumed by the player upon contact. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
    /*---------- Properties ---------*/
    [Header("Properties")]
    public bool rotate;
    public float rotationSpeed;
    
    public AudioClip collectSound;


    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
        GameMgr.inst.consumableCount++;
    }

    // Update is called once per frame
    public void Update()
    {
        if (rotate)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Collect();
        }
    }

    /* Collect Method
     * Adds armor to player, destroys gameobject on consumption
     * Removes consumable from active consumables list
     */
    public virtual void Collect()
    {
        GameMgr.inst.consumableCount--;
        Destroy(gameObject);
    }
}
