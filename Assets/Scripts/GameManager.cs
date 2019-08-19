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

        // Load player data & load level
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
        gameIsPaused = true;
        gameOverMenu.SetActive(true);
        pauseButton.SetActive(false);
        joystick.SetActive(false);
        Time.timeScale = 0f;
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
