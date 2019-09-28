using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Used if player input should be ignored. for example when the game is paused
    public bool shouldIgnoreInput;

    // Shooting
    public Transform firePoint;
    public GameObject bullet;
    public float bulletSpeed;
    public int bulletBurstCount;
    public float fireRate; 

    // Player Input
    public Joystick joystick;
    private Vector2 joystickInput;

    // Movement    
    [Range(0, .3f)]
    [SerializeField]
    private float movementSmoothing = .05f;  // How much to smooth out the movement

    [Range(10f, 50f)]
    [SerializeField]
    private float acceleration = 30f;

    private Rigidbody2D rb;
    private Vector2 currentVelocity = Vector3.zero; // Used in the SmoothDamp function

    // Animation
    private Animator animator;

    // Touch variables
    private float[] timeTouchBegan;
    private bool[] touchDidMove;
    // private float tapMovementThreshold = 1f;
    private float tapTimeThreshold = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        timeTouchBegan = new float[10];
        touchDidMove = new bool[10];

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Check if should ignore input
        if(shouldIgnoreInput)
        {
            return;            
        }

#if UNITY_EDITOR
        // Shoot
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(BurstFire());            
        }
#endif

        // Joystick Movement
        joystickInput = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;               
        
        // Touch Input        
        foreach (Touch touch in Input.touches)
        {
            int fingerIndex = touch.fingerId;

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Finger #" + fingerIndex.ToString() + " entered!");
                timeTouchBegan[fingerIndex] = Time.time;
                touchDidMove[fingerIndex] = false;                
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Finger #" + fingerIndex.ToString() + " moved!");
                touchDidMove[fingerIndex] = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                float tapTime = Time.time - timeTouchBegan[fingerIndex];
                Debug.Log("Finger #" + fingerIndex.ToString() + " left. Tap time: " + tapTime.ToString());
                if (tapTime <= tapTimeThreshold && touchDidMove[fingerIndex] == false)
                {
                    Debug.Log("Finger #" + fingerIndex.ToString() + " TAP DETECTED at: " + touch.position.ToString());
                    StartCoroutine(BurstFire());
                }
            }
        }
    }

    private void FixedUpdate()
    {   
        // Set player velocity
        Vector3 targetVelocity = joystickInput * acceleration * 10 * Time.deltaTime;
        rb.velocity = Vector2.SmoothDamp(current: rb.velocity, target: targetVelocity, currentVelocity: ref currentVelocity, smoothTime: movementSmoothing);

        // Face player with the joystick input
        if (joystickInput.magnitude > 0)
        {
            transform.up = joystickInput;
        }       
        
        // Set acceleration animation
        animator.SetBool("IsAccelerating", joystickInput.magnitude > 0);
    }

    private IEnumerator BurstFire()
    {        
        for (int i = 0; i < bulletBurstCount; i++) {
            AudioManager.Instance.Play("Bullet");
            GameObject myBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            myBullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * bulletSpeed;
            yield return new WaitForSeconds(1 / fireRate);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Draw a line from the player towards the joystick's direction
        // First create a new vector3 with joystick's horizontal and vertical. Then add the current player's position
        Vector3 toPosition = new Vector3(joystick.Horizontal, joystick.Vertical) + transform.position;
        Gizmos.DrawLine(from: transform.position, to: toPosition);

        // Show the current joystick values
        string text = joystick.Horizontal.ToString() + " " + joystick.Vertical.ToString();
        UnityEditor.Handles.Label(position: transform.position, text: text);

        // Draw a sphere where the joystick position is relative to the player
        Gizmos.DrawWireSphere(center: toPosition, radius: 0.2f);        
    }
#endif
}
