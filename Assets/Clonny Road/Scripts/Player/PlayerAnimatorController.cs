using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {

    public PlayerController playerController = null;
    private Animator animator = null;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    //private void Update()
    //{
    //    if (playerController.isDead)
    //    {
    //        animator.SetBool("dead", true);
    //    }

    //    if (playerController.jumpStart)
    //    {
    //        animator.SetBool("jumpStart", true);
    //    }
    //    else
    //    {
    //        if (!playerController.isJumping)
    //        {
    //            animator.SetBool("jump", false);
    //            animator.SetBool("jumpStart", false);
    //        }
    //    }

    //}

    public void  UpdateAnimator()
    {
        if (playerController.isDead)
        {
            animator.SetBool("dead", true);
        }

        if (playerController.isIdle)
        {
            animator.SetBool("jump", false);
            animator.SetBool("jumpStart", false);
        }

        if (playerController.jumpStart)
        {
            animator.SetBool("jumpStart", true);
        }
        else
        {
            if (playerController.isJumping)
            {
                animator.SetBool("jump", true);
                animator.SetBool("jumpStart", false);
            }
        }
        
        //else
        //{


        //    //if (!playerController.isJumping)
        //    //{
        //    //    animator.SetBool("jump", false);
        //    //    animator.SetBool("jumpStart", false);
        //    //}
        //}

    }

    public void StartJumpingEvent()
    {
        Debug.LogWarning("we register the jumping event, we will call StartMovementJump");
        //playerController.StartMovementJump();
    }
}
