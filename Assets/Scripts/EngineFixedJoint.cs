using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineFixedJoint : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Accelerate(float acceleration)
    {
        rb.AddForce(force: transform.up * acceleration);
    }
}
