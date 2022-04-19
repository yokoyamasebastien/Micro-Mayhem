// AUTHOR: Sebastien Yokoyama
// EMAIL: syokoyama2001@nevada.unr.edu
// COURSE: CS 381.1001
// ASSIGNMENT: Semester Project
// FILE NAME: InputMgr.cs
/* FILE DESCRIPTION: Manages the user's input and executes different functions depending on the received input. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : MonoBehaviour
{
    /*---------- Awake ----------*/
    // Static Instance of InputMgr for global usage
    public static InputMgr inst;
    private void Awake()
    {
        inst = this;
    }

    /*---------- Properties ----------*/
    [Header("Mouse Properties")]
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    [Header("Running")]
    bool isRunning;

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
        // Hide cursor and Lock it to center of screen
        Cursor.lockState = CursorLockMode.Locked;

        // Running is disabled by default
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*----- Mouse Input for Looking Around -----*/
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;    // Horizontal mouse movement
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;    // Vertical mouse movement

        // Up-Down Look Around
        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // Apply mouse input to camera orientation
        CameraMgr.inst.playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerMgr.inst.playerBody.Rotate(Vector3.up * mouseX);


        /*----- Keyboard Input for Movement -----*/
        // Hold LSHIFT to Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // Movement along XZ-plane
        float x = Input.GetAxis("Horizontal");  // Left-Right Movement
        float z = Input.GetAxis("Vertical");    // Forwards-Backwards Movement

        // Calculate vector for movement
        Vector3 move = PlayerMgr.inst.playerBody.transform.right * x + PlayerMgr.inst.playerBody.transform.forward * z;

        // Set speed
        float speed;

        // Check if running
        if (isRunning)
        {
            speed = PlayerMgr.inst.runSpeed;
        }
        else
        {
            speed = PlayerMgr.inst.walkSpeed;
        }

        // Apply movement vector
        PlayerMgr.inst.characterController.Move(move * speed * Time.deltaTime);

        // Jump; Only jump if player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(PlayerMgr.inst.playerBody.position, Vector3.down, PlayerMgr.inst.distToGround + 0.1f))
        {
            PlayerMgr.inst.velocity.y = Mathf.Sqrt(PlayerMgr.inst.jumpHeight * -2f * PlayerMgr.inst.gravity);
        }


        /*----- Keyboard Input for Various -----*/
        // Quit when ESC is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
