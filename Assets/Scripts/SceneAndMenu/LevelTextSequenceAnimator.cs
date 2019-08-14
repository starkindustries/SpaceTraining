using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTextSequenceAnimator : MonoBehaviour
{
    public int testLevel;
    public float charDelay;

    private List<Animator> textAnimators;
    private List<TextMeshProUGUI> levelText;

    // Start is called before the first frame update
    void Start()
    {
        // Get all animators *not* including the parent game object
        // If the parent game object is included then a warning shows that states:
        // "Parameter 'Bounce' does not exist" referring to the AnimateTextCoroutine().
        // https://answers.unity.com/questions/418600/get-components-without-parent.html        
        textAnimators = new List<Animator>();
        Animator[] animators = GetComponentsInChildren<Animator>();

        foreach (Animator animator in animators)
        {
            if (animator.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
            {
                textAnimators.Add(animator);
            }
            else
            {
                Debug.Log("LevelTextSequenceAnimator: Ignoring animator for parent game object.");
            }
        }

        // Get all text fields
        levelText = new List<TextMeshProUGUI>(GetComponentsInChildren<TextMeshProUGUI>());
        
        // Verify the text field count
        // Note that 8 refers to one text field per character: 'l' 'e' 'v' 'e' 'l' '1' '0' '0'
        if (levelText.Count == 8)
        {
            Debug.Log("Success: Level text consists of 8 text fields.");
        }
        else
        {
            Debug.LogError("Error: level text incorrectly consists of " + levelText.Count + " text fields.");
        }
    }

    public void SetLevelAndAnimate(int level)
    {
        // Verify level is between 1-999
        if (level < 1 || level > 999)
        {
            Debug.LogError("Error: invalid level: " + level);
            return;
        }

        string levelString = level.ToString();
        if (level < 10) // If level is 1 digit
        {            
            levelText[5].text = level.ToString();
            levelText[6].text = "";
            levelText[7].text = "";
        }
        else if (level < 100) // if level is 2 digits
        {
            
            levelText[5].text = levelString[0].ToString();
            levelText[6].text = levelString[1].ToString();
            levelText[7].text = "";
        }
        else // level is 3 digits
        {
            levelText[5].text = levelString[0].ToString();
            levelText[6].text = levelString[1].ToString();
            levelText[7].text = levelString[2].ToString();
        }

        // Set Slide animation trigger
        GetComponent<Animator>().SetTrigger("SlideIn");
    }

    private void AnimateText()
    {
        StartCoroutine(AnimateTextCoroutine());
    }

    private IEnumerator AnimateTextCoroutine()
    {        
        foreach (var animator in textAnimators)
        {
            animator.SetTrigger("Bounce");
            yield return new WaitForSeconds(charDelay);
        }
    }
}
