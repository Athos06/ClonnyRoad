using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveForward()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Manager.instance.GameOver();
        }
        else
        {
            Debug.Log("removing " + other.name);
            Destroy(other.gameObject);
        }


    }
}
