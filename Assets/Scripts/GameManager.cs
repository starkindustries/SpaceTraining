using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;

    private bool gameIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        // Load player data & load level
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Game Actions
    public void DidPressPause()
    {
        Debug.Log("Pause button pressed");
        gameIsPaused = true;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DidPressResume()
    {
        gameIsPaused = false;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void DidPressQuit()
    {
        Debug.Log("Quit game");
        SceneChanger.Instance.FadetoScene(sceneIndex: 0);
        Time.timeScale = 1f;
    }

    public bool GameIsPaused()
    {
        return gameIsPaused;
    }
}
