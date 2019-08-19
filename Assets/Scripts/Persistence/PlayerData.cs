using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int highscore;
    public int currentLevel;

    public PlayerData(int newHighscore, int newCurrentLevel)
    {
        highscore = newHighscore;
        currentLevel = newCurrentLevel;
    }
}
