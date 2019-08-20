using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{    
    // Pause button and menu
    public GameObject pauseButton;
    public GameObject pauseMenu;
    private bool gameIsPaused;

    // Game over menu
    public GameObject gameOverMenu;
    public TextMeshProUGUI gameOverScoreText;

    // Joystick
    public GameObject joystick;        

    // Score vars
    public TextMeshProUGUI scoreText;
    private int score;

    // Singleton pattern
    // https://gamedev.stackexchange.com/a/116010/123894
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        // Singleton Enforcement Code
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        // Hide pause and game over menus on first load
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        PlayerData data = SaveSystem.LoadPlayerData();
        score = data.currentScore;
        scoreText.text = score.ToString();
    }
    
    // Game Actions
    public void DidPressPause()
    {
        Debug.Log("Pause button pressed");
        gameIsPaused = true;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        joystick.SetActive(false);
        Time.timeScale = 0f;
    }

    public void DidPressResume()
    {
        gameIsPaused = false;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        joystick.SetActive(true);
        Time.timeScale = 1f;
    }

    public void DidPressMenu()
    {
        Debug.Log("Did press menu button.");
        SceneChanger.Instance.FadetoScene(sceneIndex: 0);
        Time.timeScale = 1f;
    }

    public void DidTriggerGameOver()
    {
        // Pause game
        gameIsPaused = true;
        Time.timeScale = 0f;

        // Show game over menu; disable others
        gameOverMenu.SetActive(true);
        pauseButton.SetActive(false);
        joystick.SetActive(false);

        // Show score
        gameOverScoreText.text = "SCORE\n" + score;

        // Hide the little score text
        scoreText.gameObject.SetActive(false);

        // Save high score
        // Load player data & load level
        PlayerData data = SaveSystem.LoadPlayerData();
        if (score > data.highscore)
        {
            SaveSystem.SavePlayerData(highscore: score, currentLevel: 0, currentScore: 0);
        }
    }

    public void SaveCurrentProgress(int currentLevel)
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        SaveSystem.SavePlayerData(highscore: data.highscore, currentLevel: currentLevel, currentScore: score);
    }

    public bool GameIsPaused()
    {
        return gameIsPaused;
    }


    public void AddToScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
