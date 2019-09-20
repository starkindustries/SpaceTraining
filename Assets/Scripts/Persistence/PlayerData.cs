using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{    
    public int currentLevel;    

    public PlayerData(int newCurrentLevel)
    {        
        currentLevel = newCurrentLevel;
    }
}
