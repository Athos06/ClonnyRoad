using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

    public PlayerController playerController = null;
    private Animator animator = null;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerController.isDead)
        {
            animator.SetBool("dead", true);
        }

        if (playerController.jumpStart)
        {
            animator.SetBool("jumpStart", true);
        }
        else
        {
            if (!playerController.isJumping)
            {
                animator.SetBool("jump", false);
                animator.SetBool("jumpStart", false);
            }
        }

    }

    public void StartJumpingEvent()
    {
        playerController.StartMovement();
    }
}
