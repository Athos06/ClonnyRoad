using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.GotOutOfBounds();
        }
        else
        {
            if(other.tag != "DeathTrigger")
            {
                ObjectPoolManager.Instance.ReturnObjectToPool(other.gameObject);
            }
        }
    }

}
