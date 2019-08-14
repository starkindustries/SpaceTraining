using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTextSequenceAnimator : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    public void SetLevelAndAnimate(int level)
    {
        levelText.text = "LEVEL " + level.ToString();
        
        // Set Slide animation trigger
        GetComponent<Animator>().SetTrigger("SlideIn");
    }    
}
