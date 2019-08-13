using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSequenceAnimator : MonoBehaviour
{
    public float charDelay;
    public float totalAnimationTime;    

    private List<Animator> textAnimators;


    // Start is called before the first frame update
    void Start()
    {
        textAnimators = new List<Animator>(GetComponentsInChildren<Animator>());
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        while (true)
        {
            float startTime = Time.time;            
            foreach (var animator in textAnimators)
            {
                animator.SetTrigger("Bounce");
                yield return new WaitForSeconds(charDelay);
            }
            float bounceTime = Time.time - startTime;            
            yield return new WaitForSeconds(totalAnimationTime - bounceTime);
        }
    }
}
