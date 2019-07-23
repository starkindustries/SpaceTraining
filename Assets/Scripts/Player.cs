using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject frontLeftEngineSprite;
    public GameObject frontRightEngineSprite;
    public GameObject rearLeftEngineSprite;
    public GameObject rearRightEngineSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // *******************
        // rear left engine
        // *******************
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A");
            //leftEngine.Accelerate(acceleration);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rearLeftEngineSprite.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            rearLeftEngineSprite.SetActive(false);
        }

        // *******************
        // rear right engine
        // *******************
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D");
            //rightEngine.Accelerate(acceleration);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rearRightEngineSprite.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            rearRightEngineSprite.SetActive(false);
        }

        // *******************
        // front left engine
        // *******************
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Q");
            //leftEngine.Accelerate(-1 * acceleration);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            frontLeftEngineSprite.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            frontLeftEngineSprite.SetActive(false);
        }

        // *******************
        // front right engine
        // *******************
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("E");
            //rightEngine.Accelerate(-1 * acceleration);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            frontRightEngineSprite.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            frontRightEngineSprite.SetActive(false);
        }
    }
}
