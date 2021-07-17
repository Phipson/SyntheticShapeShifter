using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("w")) {
            animator.SetBool("isWalking", true);
        }
        if (!Input.GetKey("w"))
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey("q"))
        {
            animator.SetBool("isWaving", true);
        }
        if (!Input.GetKey("q"))
        {
            animator.SetBool("isWaving", false);
        }

        if (Input.GetKey("e"))
        {
            animator.SetBool("isFalling", true);
        }
        if (!Input.GetKey("e"))
        {
            animator.SetBool("isFalling", false);
        }
    }
}
