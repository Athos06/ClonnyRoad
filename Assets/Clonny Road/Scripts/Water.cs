using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    bool hitWater = false;

    private void OnTriggerStay(Collider other)
    {
        if (hitWater) return;

        if(other.tag == "Player")
        {
           
            PlayerController playerController = other.GetComponent<PlayerController>();

            if(!playerController.parentedToObject)
            {
                Debug.Log("full report of state for player: " + " iddle " + playerController.isIdle + " isJumping " + playerController.isJumping );
            }

            //if(playerController.isIdle )
            //{
            //    if (!playerController.parentedToObject) {
            //        Debug.Log("position of player z is " + playerController.transform.position.z);
            //    }
            //}

            if( !playerController.parentedToObject && !playerController.isJumping)
            {
                hitWater = true;

                playerController.GotSoaked();
            }
        }
    }
}
