using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelPortal
{
    public static void GoToLevel(int level)
    {
        Debug.LogError("STOPPED HERE");
        // GameManager.Instance.SetCurrentLevel()
        SceneChanger.Instance.FadetoScene(1); // fade to endless mode scene
    }
}
