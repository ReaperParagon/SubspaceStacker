using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;
    
    private bool isPaused;
    public bool allowPauseMenu = true;

    /// Functions ///

    public void SetUIVisible(bool visible)
    {
        pauseMenuUI.SetActive(visible);
    }

    public void ShowPauseMenu(bool pause)
    {
        if (!allowPauseMenu)
            return;

        PauseGame(pause);
        SetUIVisible(pause);
    }

    public void PauseGame(bool pause)
    {
        isPaused = pause;
        Time.timeScale = (pause ? 0.0f : 1.0f);
    }

    public void TogglePause()
    {
        ShowPauseMenu(!isPaused);
    }


    /// Input System ///
    
    public void OnGamePause(InputValue value)
    {
        TogglePause();
    }

}
