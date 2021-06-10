using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject currentScorePanel;
    public float offSet = 0.05f;
    GameObject[] cylinderDelete;
    GameObject[] obstacleDelete;
    Vector3 startpos = new Vector3(0, -3.13f, 15.65f);
    ScoreManager scoreManager;
    PlayerController playerController;

    private void Awake()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    private void Start()
    {
        print(PlayerPrefs.GetInt("Level"));
    }
    void Update()
    {
        if (!playerController.startPanel.activeSelf && !gameOverPanel.activeSelf && !winPanel.activeSelf)
        {
            CheckRingMovement();
        }

    }
    /// <summary>
    /// Control current different ring size and movement 
    /// </summary>
    private void CheckRingMovement()
    {

        if (playerController.targetCylinder.gameObject.CompareTag("Obstacle"))
        {
            if (playerController.targetCylinderRadius.x + offSet > playerController.transform.localScale.x)
            {
                GameOver();
            }
        }
        if (playerController.targetCylinderRadius.x > playerController.transform.localScale.x)
        {
            GameOver();
        }
        if (playerController.isDead)
        {
            GameOver();
        }
        if (playerController.passedLevel)
        {
            Win();
        }
    }

    private void GameOver()
    {
        if (Camera.main != null)
        {
            Camera.main.GetComponent<CameraFollow>().enabled = false;
        }

        DisablePlayer();
        CheckPanels();

        if (scoreManager.collectableShape > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", scoreManager.collectableShape);
        }

    }

    void DisablePlayer()
    {
        playerController.enabled = false;
        playerController.gameObject.SetActive(false);
    }

    void CheckPanels()
    {
        currentScorePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Win()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        currentScorePanel.SetActive(false);
        winPanel.SetActive(true);

        DeleteCylinder();

        if (Camera.main != null)
        {
            Camera.main.GetComponent<CameraFollow>().enabled = false;
        }
        if (scoreManager.collectableShape > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", scoreManager.collectableShape);
        }

        playerController.passedLevel = false;
        playerController.GetComponent<PlayerController>().enabled = false;

    }

    /// <summary>
    /// After win situation or game over situation delete the current map before creating a new map.
    /// </summary>
    void DeleteCylinder()
    {
        cylinderDelete = GameObject.FindGameObjectsWithTag("Cylinder");

        if (cylinderDelete.Length > 1)
        {
            for (int i = 0; i < cylinderDelete.Length; i++)
            {
                Destroy(cylinderDelete[i]);
            }
        }

        obstacleDelete = GameObject.FindGameObjectsWithTag("Obstacle");

        if (obstacleDelete.Length >= 1)
        {
            for (int i = 0; i < obstacleDelete.Length; i++)
            {
                Destroy(obstacleDelete[i]);
            }
        }

        Destroy(GameObject.FindGameObjectWithTag("EndCylinder"));
    }

}