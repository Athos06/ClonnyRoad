using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int coinValue = 1;
    public GameObject coin = null;
    public AudioClip audioClip = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player picked up a coin!");

        Manager.instance.UpdateCoinCount(coinValue);

        coin.SetActive(false);

        this.GetComponent<AudioSource>().PlayOneShot(audioClip);

        Destroy(this.gameObject, audioClip.length);
    }
}
