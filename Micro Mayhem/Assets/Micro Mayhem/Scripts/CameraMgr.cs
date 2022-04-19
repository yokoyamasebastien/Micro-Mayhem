// AUTHOR: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: CameraMgr.cs
/* FILE DESCRIPTION: Manages the main camera in the game scene. In this case, it is the player FPS camera. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    /*---------- Awake ----------*/
    // Static Instance of CameraMgr for global usage
    public static CameraMgr inst;
    private void Awake()
    {
        inst = this;
    }

    /*---------- Properties ----------*/
    [Header("Camera Node")]
    public Camera playerCam;

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
