using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isrunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isrunningHash = Animator.StringToHash("IsRunning");

    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRun = animator.GetBool(isrunningHash);
        bool forward = Input.GetKey("w");
        bool run = Input.GetKey("left shift");


        if (!isWalking && forward)
        {
            animator.SetBool("IsWalking", true);
        }
        if (isWalking && !forward)
        {
            animator.SetBool("IsWalking", false);
        }
        if (!isRun && (forward && run))
        {
            animator.SetBool("IsRunning", true);
        }
        if (isRun && (!forward || !run))
        {
            animator.SetBool("IsRunning", false);
        }
    }
}
