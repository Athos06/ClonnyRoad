using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Manager.instance.GameOver();
        }
        else
        {
            if(other.tag != "DeathTrigger")
            { 
                Destroy(other.gameObject);
            }
        }


    }
}
