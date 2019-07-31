using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Touch lifted. Start Game!");
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse click. Start Game!");
        } 
    }
}
