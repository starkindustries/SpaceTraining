using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject title;
    public GameObject menuPanel;

    private bool isLoading;
    private bool menuPanelLoaded;

    // Start is called before the first frame update
    void Start()
    {
        isLoading = false;
        menuPanelLoaded = false;

        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If scene already loading or if menuPanelLoaded, ignore player taps
        // This prevents double clicks.
        if (isLoading || menuPanelLoaded)
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

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse click. Start Game!");
            LoadMenuPanel();
        } 
    }

    public void DidPressContinue()
    {
        AudioManager.Instance.Play("Start");
        Debug.Log("Continue");
    }

    public void DidPressNewGame()
    {
        AudioManager.Instance.Play("Start");
        Debug.Log("New game");
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

    private void LoadGameScene()
    {
        isLoading = true;
        AudioManager.Instance.Play("Start");
        SceneChanger.Instance.FadetoScene(1);
    }
}
