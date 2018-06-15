using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int coinValue = 1;
    public GameObject coin = null;
    public AudioClip audioClip = null;

	// Use this for initialization
	void Start () {
        
        //when we create the coin we check it wasnt created inside a tree
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, Vector3.up, out hit, 1.0f))
        {
            if (hit.collider.tag == "Collider")
            {
                Destroy(gameObject);
            }
        }
        Debug.DrawRay(this.transform.position, Vector3.up, Color.red, 5);

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Player picked up a coin!");
        if(other.tag == "Player")
        {
            Manager.instance.UpdateCoinCount(coinValue);

            coin.SetActive(false);

            this.GetComponent<AudioSource>().PlayOneShot(audioClip);

            Destroy(this.gameObject, audioClip.length);
        }
        
    }
}
