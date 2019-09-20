using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    public void Start()
    {
        // Get player's current level from GameManager then create the portal        
        // GameManager.Instance

        // 
        Debug.Log("success. creating level");
    }

    public static void GoToLevel(int level)
    {
        Debug.LogError("STOPPED HERE");
        // GameManager.Instance.SetCurrentLevel()
        SceneChanger.Instance.FadetoScene(1); // fade to endless mode scene
    }
}
