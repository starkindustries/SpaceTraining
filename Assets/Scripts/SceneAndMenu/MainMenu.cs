using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject title;
    public GameObject menuPanel;
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

        // Get Player Data
        data = SaveSystem.LoadPlayerData();

        // Set the start button text        
        startButtonText.text = "START LEVEL " + data.currentLevel;
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
