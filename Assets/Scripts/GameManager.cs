using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerData data;

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

        // Don't Destroy On Load
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {       
        // Load Player data from save file
        data = SaveSystem.LoadPlayerData();

        // Check if data is null
        if (data == null)
        {
            // Null data. Create new save file.
            Debug.Log("No save file. Creating one now..");
            // SaveSystem.SavePlayerData(highscore: 0, currentLevel: 1, currentScore: 0);
            // data = SaveSystem.LoadPlayerData();
        }
        else
        {
            Debug.Log("Save file found. Player data loaded successfully!");
        }        
    }
}
