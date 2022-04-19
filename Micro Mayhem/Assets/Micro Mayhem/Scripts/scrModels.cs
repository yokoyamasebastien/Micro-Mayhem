// NAME: Sebastien Yokoyama
// COURSE: CS 381.1001
// ASSIGNMENT: Micro Mayhem
// FILE NAME: scrModels.cs
/* FILE DESCRIPTION: Contains code that handles player control settings. */

/***************************************************************************************
* Title: scrModels source code
* Author: "Fuelled By Caffeine"
* Date: 2021
* Availability: https://www.youtube.com/playlist?list=PLW3-6V9UKVh2T0wIqYWC1qvuCk2LNSG5c
*
***************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class scrModels
{
    #region - Player -

    [Serializable]
    public class PlayerSettingsModel
    {
        [Header("View Settings")]
        public float viewXSensitivity;
        public float viewYSensitivity;

        public bool viewXInverted;
        public bool viewYInverted;

        [Header("Movement Settings")]
        public bool sprintingHold;
        public float movementSmoothing;

        [Header("Movement - Running")]
        public float runningForwardSpeed;
        public float runningStrafeSpeed;

        [Header("Movement - Walking")]
        public float walkingForwardSpeed;
        public float walkingBackwardSpeed;
        public float walkingStrafeSpeed;

        [Header("Jumping")]
        public float jumpingHeight;
        public float jumpingFalloff;
        public float fallingSmoothing;

        [Header("Speed Effectors")]
        public float speedEffector = 1;
        public float fallingSpeedEffector;
    }

    #endregion
}
