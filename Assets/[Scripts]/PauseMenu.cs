using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;
    
    private bool isPaused;

    /// Functions ///

    public void SetUIVisible(bool visible)
    {
        pauseMenuUI.SetActive(visible);
    }

    public void PauseGame(bool pause)
    {
        isPaused = pause;
        SetUIVisible(isPaused);
        Time.timeScale = (pause ? 0.0f : 1.0f);
    }

    public void TogglePause()
    {
        PauseGame(!isPaused);
    }


    /// Input System ///
    
    public void OnGamePause(InputValue value)
    {
        TogglePause();
    }

}
