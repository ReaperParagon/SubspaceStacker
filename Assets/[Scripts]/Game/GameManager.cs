using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives;

    [SerializeField]
    private GameObject resultsUI;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI resultsScoreText;

    [SerializeField]
    private HealthUIManager healthUI;

    [SerializeField]
    private PauseMenu pauseMenu;

    private int score;

    private void OnEnable()
    {
        DeathPlane.OnObjectFell += LoseLife;
        ObjectSpawner.OnObjectSpawn += AddScore;
    }

    private void OnDisable()
    {
        DeathPlane.OnObjectFell -= LoseLife;
        ObjectSpawner.OnObjectSpawn -= AddScore;
    }

    /// Functions ///

    private void LoseLife()
    {
        if (--lives <= 0)
            EndGame();

        // TODO: Play sound effects / Particle effects

        UpdateHealthUI();
    }

    private void AddScore()
    {
        score++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        string scoreString = "Stack: " + score.ToString();
        resultsScoreText.text = scoreString;
        scoreText.text = scoreString;
    }

    private void UpdateHealthUI()
    {
        healthUI.UpdateUI(lives);
    }

    private void EndGame()
    {
        resultsUI.SetActive(true);

        pauseMenu.PauseGame(true);
        pauseMenu.allowPauseMenu = false;

        UpdateScoreUI();
    }

}
