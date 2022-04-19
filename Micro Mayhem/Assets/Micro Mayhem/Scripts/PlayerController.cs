// AUTHOR: Sebastien Yokoyama
// COURSE: CS 381.1001
// ASSIGNMENT: Micro Mayhem
// FILE NAME: PlayerController.cs
/* FILE DESCRIPTION: Contains the code that handles player controls, using Unity's Input System V1.3.0. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static scrModels;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private DefaultInput defaultInput;

    private Vector3 newCameraRotation;
    private Vector3 newPlayerRotation;

    public Vector2 input_Movement;
    public Vector2 input_View;

    [Header("References")]
    public Transform playerCamera;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public float viewClampYMin = -70;
    public float viewClampYMax = 80;

    [Header("Gravity")]
    public float gravityAmount;
    public float gravityMin;
    private float playerGravity;

    public Vector3 jumpingForce;
    private Vector3 jumpingForceVelocity;

    private bool isSprinting;

    private Vector3 newMovementSpeed;
    private Vector3 newMovementVelocity;

    private void Awake()
    {
        defaultInput = new DefaultInput();

        defaultInput.Character.Movement.performed += e => input_Movement = e.ReadValue<Vector2>();
        defaultInput.Character.View.performed += e => input_View = e.ReadValue<Vector2>();
        defaultInput.Character.Jump.performed += e => Jump();
        defaultInput.Character.Sprint.performed += e => ToggleSprint();
        defaultInput.Character.SprintReleased.performed += e => StopSprint();

        defaultInput.Enable();

        newCameraRotation = playerCamera.localRotation.eulerAngles;
        newPlayerRotation = transform.localRotation.eulerAngles;

        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CalculateView();
        CalculateMovement();
        CalculateJump();
    }

    private void CalculateView()
    {
        newPlayerRotation.y += playerSettings.viewXSensitivity * (playerSettings.viewXInverted ? -input_View.x : input_View.x) * Time.deltaTime;
        transform.rotation = Quaternion.Euler(newPlayerRotation);

        newCameraRotation.x += playerSettings.viewYSensitivity * (playerSettings.viewYInverted ? input_View.y : -input_View.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);

        playerCamera.localRotation = Quaternion.Euler(newCameraRotation);
    }

    private void CalculateMovement()
    {
        if (input_Movement.y <= 0.2f)
        {
            isSprinting = false;
        }

        var verticalSpeed = playerSettings.walkingForwardSpeed;
        var horizontalSpeed = playerSettings.walkingStrafeSpeed;

        if (isSprinting)
        {
            verticalSpeed = playerSettings.runningForwardSpeed;
            horizontalSpeed = playerSettings.runningStrafeSpeed;
        }

        // Effectors
        if (!characterController.isGrounded)
        {
            playerSettings.speedEffector = playerSettings.fallingSpeedEffector;
        }
        else
        {
            playerSettings.speedEffector = 1;
        }

        verticalSpeed *= playerSettings.speedEffector;
        horizontalSpeed *= playerSettings.speedEffector;

        newMovementSpeed = Vector3.SmoothDamp(newMovementSpeed, 
            new Vector3(horizontalSpeed * input_Movement.x * Time.deltaTime, 0, verticalSpeed * input_Movement.y * Time.deltaTime), 
            ref newMovementVelocity, (characterController.isGrounded ? playerSettings.movementSmoothing : playerSettings.fallingSmoothing));
        var MovementSpeed = transform.TransformDirection(newMovementSpeed);

        if (playerGravity > gravityMin) 
        { 
            playerGravity -= gravityAmount * Time.deltaTime;
        }

        if(playerGravity < -0.1f && characterController.isGrounded) 
        { 
            playerGravity = -0.1f; 
        }

        MovementSpeed.y += playerGravity;
        MovementSpeed += jumpingForce * Time.deltaTime;

        characterController.Move(MovementSpeed);
    }

    private void CalculateJump()
    {
        jumpingForce = Vector3.SmoothDamp(jumpingForce, Vector3.zero, ref jumpingForceVelocity, playerSettings.jumpingFalloff);
    }

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

    private void ToggleSprint()
    {
        if (input_Movement.y <= 0.2f)
        {
            isSprinting = false;
            return;
        }

        isSprinting = !isSprinting;
    }

    private void StopSprint()
    {
        if (playerSettings.sprintingHold)
        {
            isSprinting = false;
        }
    }
}
