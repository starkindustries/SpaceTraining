using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int highscore;
    public int currentLevel;
    public int currentScore;

    public PlayerData(int newHighscore, int newCurrentLevel, int newCurrentScore)
    {
        highscore = newHighscore;
        currentLevel = newCurrentLevel;
        currentScore = newCurrentScore;
    }
}
