using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject title;
    public GameObject menuPanel;
    public TextMeshProUGUI highscoreNumberText;
    public TextMeshProUGUI startButtonText;

    private bool menuPanelLoaded;

    private PlayerData data;

    // Start is called before the first frame update
    void Start()
    {
        // Reset menuPanelLoaded boolean
        menuPanelLoaded = false;

        // Show title and disable menuPanel on first load
        title.SetActive(true);
        menuPanel.SetActive(false);

        // TODO: delete this line:
        // SaveSystem.SavePlayerData(highscore: 0, currentLevel: 0);

        // Load Player data from save file
        data = SaveSystem.LoadPlayerData();
        if (data == null)
        {
            // start new level
            Debug.Log("No save file. Creating one now..");
            SaveSystem.SavePlayerData(highscore: 0, currentLevel: 0);
            data = SaveSystem.LoadPlayerData();
        }
        else
        {
            Debug.Log("Save file found. Load player data!");
        }
        Debug.Log("Current level: " + data.currentLevel);
        Debug.Log("high score: " + data.highscore);

        // Set the highscore number text
        if (data.highscore > 0)
        {
            highscoreNumberText.text = data.highscore.ToString();
        }
        else
        {
            highscoreNumberText.text = "";
        }

        // Set the start button text
        if (data.currentLevel > 1)
        {
            startButtonText.text = "CONTINUE";
        }
        else
        {
            startButtonText.text = "NEW GAME";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If menuPanelLoaded, ignore player taps. This prevents rogue taps 
        // in the menuPanel, where only buttons should be tapped.
        if (menuPanelLoaded)
        {
            return;
        }

        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {                
                Debug.Log("Touch lifted. Start Game!");
                LoadMenuPanel();
            }
        }

#if UNITY_EDITOR
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse click. Start Game!");
            LoadMenuPanel();
        }
#endif
    }

    public void DidPressStart()
    {
        Debug.Log("Start");
        AudioManager.Instance.Play("Start");        
        SceneChanger.Instance.FadetoScene(1);
    }    

    // When player first taps space breaker
    // Load the main menu
    private void LoadMenuPanel()
    {
        menuPanelLoaded = true;
        AudioManager.Instance.Play("Start");
        title.SetActive(false);
        menuPanel.SetActive(true);
    }    
}
