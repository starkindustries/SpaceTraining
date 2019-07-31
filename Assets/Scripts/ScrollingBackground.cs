using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed;
    public int offsetMultiplier;

    private float backgroundWidth;
    private float xBoundary;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        xBoundary = screenBounds.x;

        backgroundWidth = GetComponent<SpriteRenderer>().size.x;
        
        // Set scroll speed velocity
        GetComponent<Rigidbody2D>().velocity = new Vector2(-scrollSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float offscreenPosition = xBoundary + backgroundWidth / 2;
        if (transform.position.x < offscreenPosition * -1)
        {
            RepositionBackground();
        }        
    }

    private void RepositionBackground()
    {
        Vector2 groundOffset = new Vector2(backgroundWidth * offsetMultiplier, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
