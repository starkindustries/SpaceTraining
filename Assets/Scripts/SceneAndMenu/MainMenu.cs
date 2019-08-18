using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private bool isLoading;

    // Start is called before the first frame update
    void Start()
    {
        isLoading = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If scene already loading, ignore player input.
        // This prevents double clicks.
        if (isLoading)
        {
            return;
        }

        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {                
                Debug.Log("Touch lifted. Start Game!");
                LoadGameScene();
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse click. Start Game!");
            LoadGameScene();
        } 
    }

    private void LoadGameScene()
    {
        isLoading = true;
        AudioManager.Instance.Play("Start");
        SceneChanger.Instance.FadetoScene(1);
    }
}
