using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool gameIsPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (gameIsPaused && pauseMenuUI != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        { 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        UIMgr.inst.playerHealth.enabled = true;
        UIMgr.inst.playerArmor.enabled = true;
        UIMgr.inst.playerAmmo.enabled = true;
        UIMgr.inst.timer.enabled = true;
        UIMgr.inst.waveNumber.enabled = true;
        UIMgr.inst.crosshair.enabled = true;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        UIMgr.inst.playerHealth.enabled = false;
        UIMgr.inst.playerArmor.enabled = false;
        UIMgr.inst.playerAmmo.enabled = false;
        UIMgr.inst.timer.enabled = false;
        UIMgr.inst.waveNumber.enabled = false;
        UIMgr.inst.crosshair.enabled = false;
        gameIsPaused = true;
    }

    public void resumeGame()
    {
        Resume();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
