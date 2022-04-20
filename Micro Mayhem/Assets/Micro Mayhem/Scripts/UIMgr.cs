// AUTHOR: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: UIMgr.cs
/* FILE DESCRIPTION: Manages the UI elements in the game, such as Player Health and Armor */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Image
using TMPro;    // TextMeshPro

public class UIMgr : MonoBehaviour
{
    /*---------- AWAKE ----------*/
    // Static instance of UIMgr for global usage
    public static UIMgr inst;
    private void Awake()
    {
        inst = this;
    }


    /*---------- Properties ----------*/
    public TextMeshProUGUI playerHealth;
    public TextMeshProUGUI playerArmor;
    public Image crosshair;

    public TextMeshProUGUI timer;
    private float time;

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
        /*----- Player Attributes -----*/
        // Set Text Size
        playerHealth.fontSize = 60;
        playerArmor.fontSize = 60;

        // Set Text Color
        // Player Health Color is Red
        playerHealth.color = new Color32(255, 0, 0, 255);
        // Player Armor Color is Blue
        playerArmor.color = new Color32(0, 255, 255, 255);

        // Set Text Outline thicknesss and color (black outline)
        playerHealth.outlineWidth = 0.2f;
        playerHealth.outlineColor = Color.gray;

        playerArmor.outlineWidth = 0.2f;
        playerArmor.outlineColor = Color.gray;

        // Get and Set values
        SetPlayerAttributes();

        /*----- Timer -----*/
        // Set Text Size
        timer.fontSize = 60;

        // Set color
        timer.color = new Color32(0, 255, 0, 255);

        // Set Text Outline thickness and color
        timer.outlineWidth = 0.2f;
        timer.outlineColor = Color.gray;

        // Set Time value and text
        SetTime();
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerAttributes();
        SetTime();
    }

    /* SetPlayerAttributes Method
    Accesses the PlayerMgr to retrieve the player's attribute values and set the text to reflect those values. 
    */
    void SetPlayerAttributes()
    {
        playerHealth.text = PlayerMgr.inst.health.ToString();
        playerArmor.text = PlayerMgr.inst.armor.ToString();
    }

    void SetTime()
    {
        float time = Time.timeSinceLevelLoad;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time - minutes * 60);

        string min, sec;

        if (minutes < 10)
        {
            min = "0" + minutes.ToString();
        }
        else
        {
            min = minutes.ToString();
        }

        if (seconds < 10)
        {
            sec = "0" + seconds.ToString();
        }
        else
        {
            sec = seconds.ToString();
        }

        timer.text = min + ":" + sec;
    }
}
