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

    private Renderer myRenderer = null;
    private bool isVisible = false;

	// Use this for initialization
	void Start () {
        myRenderer = moverObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(speed * Time.deltaTime, 0, 0);
        IsVisible();
	}

    void IsVisible()
    {
        if (myRenderer.isVisible)
        {
            isVisible = true;
        }
        if(!myRenderer.isVisible && isVisible )
        {
            Destroy(this.gameObject);
        }
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
                Debug.Log("Enter: gothit");

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
