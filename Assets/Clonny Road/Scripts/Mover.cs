using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1.0f;
    public float moveDirection = 0;
    public bool parentOnTrigger = true;
    public bool hitBoxOnTrigger = false;

    public GameObject moverObject = null;
    	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(speed * Time.deltaTime, 0, 0);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            PlayerController player = other.GetComponent<PlayerController>();

            if (parentOnTrigger)
            {
            
                other.transform.parent = this.transform;

                if (!player.isDead)
                    player.parentedToObject = true;
            }

            if (hitBoxOnTrigger)
            {
                player.GotHit();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {   
            if( parentOnTrigger )
            {
                Debug.Log("exit");

                other.transform.parent = null;

                other.GetComponent<PlayerController>().parentedToObject = false;
            }
        }
    }
}
