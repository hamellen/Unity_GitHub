using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_1D : MonoBehaviour
{
    public Animator animator;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float offset = 0.5f + Input.GetAxis("Run") * 0.5f;
        float moveParameter = Mathf.Abs(vertical * offset);

        animator.SetFloat("Movespeed", moveParameter);

    }
}
