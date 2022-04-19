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
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    /*---------- Methods ----------*/
    // Start is called before the first frame update
    void Start()
    {
        // Hide cursor and Lock it to center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        /*----- Mouse Input -----*/
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;    // Horizontal mouse movement
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;    // Vertical mouse movement

        // Up-Down Look Around
        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // Apply mouse input to camera orientation
        CameraMgr.inst.playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerMgr.inst.playerBody.Rotate(Vector3.up * mouseX);

        /*----- Keyboard Input -----*/
        // Movement along XZ-plane
        float x = Input.GetAxis("Horizontal");  // Left-Right Movement
        float z = Input.GetAxis("Vertical");    // Forwards-Backwards Movement

        // Calculate vector for movement
        Vector3 move = PlayerMgr.inst.playerBody.transform.right * x + PlayerMgr.inst.playerBody.transform.forward * z;

        // Apply movement vector
        PlayerMgr.inst.characterController.Move(move * PlayerMgr.inst.speed * Time.deltaTime);

        // Jump; Mapped to SPACE
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerMgr.inst.velocity.y = Mathf.Sqrt(PlayerMgr.inst.jumpHeight * -2f * PlayerMgr.inst.gravity);
        }
    }
}
