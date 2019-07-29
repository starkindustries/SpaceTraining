using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    // Shooting
    public Transform firePoint;
    public GameObject bullet;
    public float bulletSpeed;

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
    private bool isAccelerating;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {        
        // Shoot        
        if (Input.GetButtonDown("Jump"))
        {
            GameObject myBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            myBullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * bulletSpeed;
        }
        
        // Joystick Movement
        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            joystickInput = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;            
        }               
    }

    private void FixedUpdate()
    {   
        // THIS WORKS
        Vector3 targetVelocity = joystickInput * acceleration * 10 * Time.deltaTime;
        rb.velocity = Vector2.SmoothDamp(current: rb.velocity, target: targetVelocity, currentVelocity: ref currentVelocity, smoothTime: movementSmoothing);
        transform.up = joystickInput;
        animator.SetBool("IsAccelerating", isAccelerating);
    }

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
}
