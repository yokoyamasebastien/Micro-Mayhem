// NAME: Sebastien Yokoyama
// COURSE: CS 381.1001
// ASSIGNMENT: Micro Mayhem
// FILE NAME: PlayerController.cs
/* FILE DESCRIPTION: Contains the code that handles player controls, using Unity's Input System V1.3.0. */

/***************************************************************************************
* Title: Scr_Character Controller source code
* Author: "Fuelled By Caffeine"
* Date: 2021
* Availability: https://www.youtube.com/playlist?list=PLW3-6V9UKVh2T0wIqYWC1qvuCk2LNSG5c
*
***************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static scrModels;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;    // Uses the Player GameObject's Character Controller Component
    private DefaultInput defaultInput;  // Uses the DefaultInput Script generated from the Unity Input System package

    private Vector3 newCameraRotation;  // Used for looking around
    private Vector3 newPlayerRotation;  // Used for turning the player, when considering the movement key pressed and camera orientation

    public Vector2 input_Movement;  // Used for player movement
    public Vector2 input_View;  // Used for player view movement

    [Header("References")]
    public Transform playerCamera;  // Uses a camera node ("Player Camera") attached to the Player GameObject

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;  // Instance of PlayerSettingsModel Class from scrModels source file
    public float viewClampYMin = -70;   // Minimum in range of vertical camera motion
    public float viewClampYMax = 80;    // Maximum in range of vertical camera motion

    [Header("Gravity")]
    public float gravityAmount; // Amount of gravity applied on player
    public float gravityMin;    // Minimum amount of gravity
    private float playerGravity;    // Gravity that player has

    public Vector3 jumpingForce;    // Force of player jumping
    private Vector3 jumpingForceVelocity;   // Velocity of player when jumping

    [Header("Movement")]
    private bool isSprinting;   // Boolean to determine if player is running

    private Vector3 newMovementSpeed;   // Movement speed of player
    private Vector3 newMovementVelocity;    // Movement velocity of player

    /* Awake Method
    Initializes player user input
    Initializes player camera orientation
    Initializes player character orientation
    Attaches Player GameObject Character Controller Component to this Script
    */
    private void Awake()
    {
        // Initialize player user input from DefaultInput Script
        defaultInput = new DefaultInput();

        defaultInput.Character.Movement.performed += e => input_Movement = e.ReadValue<Vector2>();
        defaultInput.Character.View.performed += e => input_View = e.ReadValue<Vector2>();
        defaultInput.Character.Jump.performed += e => Jump();
        defaultInput.Character.Sprint.performed += e => ToggleSprint();
        defaultInput.Character.SprintReleased.performed += e => StopSprint();

        defaultInput.Enable();

        // Set player camera and character orientation
        newCameraRotation = playerCamera.localRotation.eulerAngles;
        newPlayerRotation = transform.localRotation.eulerAngles;

        // Attach Player GameObject Character Controller Component
        characterController = GetComponent<CharacterController>();
    }

    /* Update Method
    Called for every frame
    Calls the CalculateView Method to calculate the player camera's orientation.
    Calls the CalculateMovement Method to calculate the player's position along the XZ-plane.
    Calls the CalculateJump Method to calculate the player's position along the Y-axis.
    */
    private void Update()
    {
        CalculateView();
        CalculateMovement();
        CalculateJump();
    }

    /* CalculateView Method
    Calculates the player camera's orientation based on mouse movement.
    */
    private void CalculateView()
    {
        // Set player's camera horizontal orientation and player's physical horizontal orientation for movement
        newPlayerRotation.y += playerSettings.viewXSensitivity * (playerSettings.viewXInverted ? -input_View.x : input_View.x) * Time.deltaTime;
        transform.rotation = Quaternion.Euler(newPlayerRotation);

        // Set player's camera vertical orientation
        newCameraRotation.x += playerSettings.viewYSensitivity * (playerSettings.viewYInverted ? input_View.y : -input_View.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);

        // Apply the horizontal and vertical orientations
        playerCamera.localRotation = Quaternion.Euler(newCameraRotation);
    }

    /* CalculateMovement Method
    Calculate the player's position based on keyboard input.
    */
    private void CalculateMovement()
    {
        // Set player's "sprinting state" to 0 if not moving
        if (input_Movement.y <= 0.2f)
        {
            isSprinting = false;
        }

        // Set speed
        var verticalSpeed = playerSettings.walkingForwardSpeed;
        var horizontalSpeed = playerSettings.walkingStrafeSpeed;

        // Set speed if player is sprinting
        if (isSprinting)
        {
            verticalSpeed = playerSettings.runningForwardSpeed;
            horizontalSpeed = playerSettings.runningStrafeSpeed;
        }

        // Speed Effectors for special states
        // If player is mid-air (jumping/falling) use falling speed effector
        if (!characterController.isGrounded)
        {
            playerSettings.speedEffector = playerSettings.fallingSpeedEffector;
        }
        // If player is on ground, normally, use the default speed effector
        else
        {
            playerSettings.speedEffector = 1;
        }

        // Apply speed effectors
        verticalSpeed *= playerSettings.speedEffector;
        horizontalSpeed *= playerSettings.speedEffector;

        // Calculate new XZ movement speed
        newMovementSpeed = Vector3.SmoothDamp(newMovementSpeed, 
            new Vector3(horizontalSpeed * input_Movement.x * Time.deltaTime, 0, verticalSpeed * input_Movement.y * Time.deltaTime), 
            ref newMovementVelocity, (characterController.isGrounded ? playerSettings.movementSmoothing : playerSettings.fallingSmoothing));

        // Apply new XZ movement speed to current XZ movement speed
        var MovementSpeed = transform.TransformDirection(newMovementSpeed);

        // Gradually decrease player's gravity, to allow falling down
        if (playerGravity > gravityMin) 
        { 
            playerGravity -= gravityAmount * Time.deltaTime;
        }

        // Sets gravity to near-zero if on ground
        if(playerGravity < -0.1f && characterController.isGrounded) 
        { 
            playerGravity = -0.1f; 
        }

        // Adjust player's Y speed based on their gravity
        MovementSpeed.y += playerGravity;
        MovementSpeed += jumpingForce * Time.deltaTime;

        // Apply XYZ movement speed
        characterController.Move(MovementSpeed);
    }

    /* CalculateJump Method
    Calculates the player's jumping force based on player settings
    */
    private void CalculateJump()
    {
        jumpingForce = Vector3.SmoothDamp(jumpingForce, Vector3.zero, ref jumpingForceVelocity, playerSettings.jumpingFalloff);
    }

    /* Jump Method
    Checks if the player is not on ground, returns false if so. (You can't jump if you're already in mid-air)
    Calculate jumping force based on jumping height
    */
    private void Jump()
    {
        if (!characterController.isGrounded)
        {
            return;
        }

        // Jump
        jumpingForce = Vector3.up * playerSettings.jumpingHeight;
        playerGravity = 0;
    }

    /* ToggleSprint Method
    Adjusts the player's state of sprinting if sprint is using toggle option
    */
    private void ToggleSprint()
    {
        // Set sprint state to false when not moving (essentially resets it when you stop moving)
        if (input_Movement.y <= 0.2f)
        {
            isSprinting = false;
            return;
        }

        // Change state to opposite when toggled
        isSprinting = !isSprinting;
    }

    /* StopSprint Method
    Adjusts the player's sprinting state to false.
    Method is used when sprint is using hold down option
    */
    private void StopSprint()
    {
        if (playerSettings.sprintingHold)
        {
            isSprinting = false;
        }
    }
}
