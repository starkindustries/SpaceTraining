using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public EngineFixedJoint leftEngine;
    public EngineFixedJoint rightEngine;

    public GameObject frontLeftEngineSprite;
    public GameObject frontRightEngineSprite;
    public GameObject rearLeftEngineSprite;
    public GameObject rearRightEngineSprite;
    public float acceleration;

    public Transform firePoint;
    public GameObject bullet;
    public float bulletSpeed;    

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
            leftEngine.Accelerate(acceleration);
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
            rightEngine.Accelerate(acceleration);
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
            leftEngine.Accelerate(-1 * acceleration);
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
            rightEngine.Accelerate(-1 * acceleration);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            frontRightEngineSprite.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            frontRightEngineSprite.SetActive(false);
        }

        // *******************
        // Shoot
        // *******************
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject myBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            myBullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * bulletSpeed;
        }
    }
}
