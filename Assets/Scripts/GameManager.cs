using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerData playerData;

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

        // Load Player data from save file        
        playerData = SaveSystem.LoadPlayerData();

        // Check if data is null
        if (playerData == null)
        {
            // Null data. Create new save file.
            Debug.Log("No save file. Creating one now..");
            playerData = new PlayerData(newCurrentLevel: 1);
            SaveSystem.SavePlayerData(playerData);            
        }
        else
        {
            Debug.Log("Save file found. Player data loaded successfully!");
        }
    }
    
    public PlayerData GetPlayerData()
    {
        return playerData;
    }
}
