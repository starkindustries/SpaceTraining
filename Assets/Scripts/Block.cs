using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private int hitPoints;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("block struct. hit points: " + hitPoints);
        hitPoints--;
        animator.SetTrigger("Shake");
        if (hitPoints < 1)
        {
            Destroy(this.gameObject);
        }       
    }
}
