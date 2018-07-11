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

    private Collider testColl;
    private void Awake()
    {
        testColl = GetComponent<Collider>();    
    }

  
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
                if (!player.isDead) { 
                    other.transform.parent = this.transform;
                    player.parentedToObject = true;
                    //Debug.LogWarning("we are parenting to log " + gameObject.name + " " + gameObject.GetInstanceID() );
                }
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
                PlayerController player = other.GetComponent<PlayerController>();
                if (player.isDead)
                    return; 

                //Debug.LogWarning("exit from " + gameObject.name + " " + gameObject.GetInstanceID());

                //something happens when sometimes OnTriggerExit is called after OnTriggerEnter from another log is triggered, so actually we unparent the character anyway, when it should stay parented to the other log and thats it MUY MALO MALO
                if(other.transform.parent == this.transform) { 
                    other.transform.parent = null;
                    other.GetComponent<PlayerController>().parentedToObject = false;
                }
              
            }
        }
    }
}
