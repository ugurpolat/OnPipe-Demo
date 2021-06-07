using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    ScoreManager scoreManager;

    [Header("Current Score Panel")]
    public Text currentScoreText;
    public Text currentScorePanelLevelText;

    [Header("Game Over Panel")]
    public Text levelTextGameOverPanel;
    public Text bestScoreTextGameOver;
    public Text gameOverScoreText;

    [Header("Win Panel")]
    public Text levelTextWinPanel;
    public Text bestScoreWinPanel;
    public Text scoreTextWinPanel;

    private void Awake()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        scoreManager.ScoreUpdated += ScoreManager_ScoreUpdated;
        GetLevelNumber();
    }

    private void ScoreManager_ScoreUpdated(int pointValue)
    {
        UpdateScore(pointValue);
    }

    // updating score for diffent panel
    private void UpdateScore(int score)
    {
        currentScoreText.text = score.ToString();
        gameOverScoreText.text = score.ToString();
        bestScoreWinPanel.text = score.ToString();
        scoreTextWinPanel.text = score.ToString();
        bestScoreTextGameOver.text = "BEST " + PlayerPrefs.GetInt("HighScore");
    }

    // update level number
    void GetLevelNumber()
    {
        levelTextWinPanel.text = PlayerPrefs.GetInt("Level").ToString();
        levelTextGameOverPanel.text = PlayerPrefs.GetInt("Level").ToString();
        currentScorePanelLevelText.text = PlayerPrefs.GetInt("Level").ToString();
    }

}
