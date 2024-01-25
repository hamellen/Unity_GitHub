using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy_armor_controller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0)
        {
            animator.SetBool("IsMove", true);
        }
        else if(rb.velocity.magnitude == 0)
        {
            animator.SetBool("IsMove", false);
        }
    }
}
