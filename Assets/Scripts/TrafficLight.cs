using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public GameObject redLight = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "train")
        {
            redLight.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "train")
        {
            redLight.SetActive(false);
        }
    }
}
