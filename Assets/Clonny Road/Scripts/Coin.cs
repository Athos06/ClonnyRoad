using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int coinValue = 1;
    public Coin coin = null;
    public MeshRenderer meshRenderer;

    public AudioClip audioClip = null;

    void OnDisable()
    {
        
    }

    void OnEnable()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        meshRenderer.enabled = true;

        yield return new WaitForEndOfFrame();

        //layer 11 is the ground layer
        int layerMask = 1 << 11;
        layerMask = ~layerMask;


        //when we create the coin we check it wasnt created inside a tree
        RaycastHit hit;
        Vector3 posRay = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 7, layerMask))
        {
            if (hit.collider.tag == "Collider")
            {
                ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
            }
        }
        Debug.DrawRay(transform.position, Vector3.up*10, Color.red, 500);
    }
    ////since we use the Pooling system OnEnable is used instead of Start for initialization
    //private void OnEnable()
    //{
    //    //when we create the coin we check it wasnt created inside a tree
    //    RaycastHit hit;
    //    if (Physics.Raycast(this.transform.position, Vector3.up, out hit, 10.0f))
    //    {
    //        Debug.Log("enter in raycast");
    //        if (hit.collider.tag == "Collider")
    //        {
    //            Debug.Log("tree detected in place deleting");
    //            ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
    //        }

    //        if (hit.collider.tag == "Coin")
    //        {
    //            Debug.Log("coin detected in place deleting");
    //            ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
    //        }
    //    }
    //    Debug.DrawRay(this.transform.position, Vector3.up, Color.red, 5);
    //}



    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Manager.Instance.UpdateCoinCount(coinValue);



            this.GetComponent<AudioSource>().PlayOneShot(audioClip);
            this.coin.enabled = false;
            meshRenderer.enabled = false;

            ObjectPoolManager.Instance.ReturnObjectToPool(gameObject, audioClip.length);


            //TODO problemo here, set active false return the coin to the pool , 
            //coin.enabled = false;
        }

    }

    private void OnDestroy()
    {
        Debug.Log("coin " + gameObject.name + "was destroyed and shouldnt");
    }

}
