using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Manager.Instance.GameOver();
        }
        else
        {
            if(other.tag != "DeathTrigger")
            {
                if(other.tag == "Ground")
                {
                    Destroy(other.gameObject);
                }
                else
                {
                    ObjectPoolManager.Instance.ReturnObjectToPool(other.gameObject);
                }
            }
        }


    }
}
