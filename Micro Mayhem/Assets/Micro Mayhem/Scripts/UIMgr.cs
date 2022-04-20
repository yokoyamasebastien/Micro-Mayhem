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

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerAttributes();
    }

    /* SetPlayerAttributes Method
    Accesses the PlayerMgr to retrieve the player's attribute values and set the text to reflect those values. 
    */
    void SetPlayerAttributes()
    {
        playerHealth.text = PlayerMgr.inst.health.ToString();
        playerArmor.text = PlayerMgr.inst.armor.ToString();
    }
}
