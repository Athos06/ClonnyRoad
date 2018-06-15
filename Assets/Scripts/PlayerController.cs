using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveDistance = 1;
    public float moveTime = 0.4f;
    public float colliderDistCheck = 1;

    public bool isIdle = true;
    public bool isDead = false;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool jumpStart = false;

    public ParticleSystem particle = null;
    public ParticleSystem splash = null;
    public GameObject chick = null;

    private Renderer myRenderer = null;
    private bool isVisible = false;

    public AudioClip audioIdle1 = null;
    public AudioClip audioIdle2 = null;
    public AudioClip audioHop = null;
    public AudioClip audioHit = null;
    public AudioClip audioSplash = null;

    
    public bool parentedToObject = false;


    public bool enableAngleCheckOnMove = true;
    public float angleCheck = 1;
    public float angleCheckDist = 0.5f;


    private float mostAdvancedPosition;

    private void Start()
    {
        mostAdvancedPosition = transform.position.z;

        myRenderer = chick.GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!Manager.instance.CanPlay()) return;
            
        if (isDead) return;

        CanIdle();

        CanMove();

        IsVisible();
    }

    void CanIdle()
    {
        if (isIdle)
        {
            if( Input.GetKeyDown (KeyCode.UpArrow ) || 
                Input.GetKeyDown (KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow))
            {

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    CheckIfIdle(270, 0, 0);
                    //gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    CheckIfIdle(270, 180, 0);
                    //gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    CheckIfIdle(270, -90, 0);
                    //gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    CheckIfIdle(270, 90, 0);
                    //gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                }

                //CheckIfCanMove();

                //PlayAudioClip(audioIdle1);
                
            }
        }
    }

    void CheckIfIdle(float x, float y, float z)
    {
        chick.transform.rotation = Quaternion.Euler(x, y, z);

        if( enableAngleCheckOnMove)
        {
            CheckIfCanMoveAngles();
        }
        else
        {
            CheckIfCanMove();
        }
        
        int a = Random.Range(0, 12);
        if (a < 4) PlayAudioClip(audioIdle1);
    }

    void CheckIfCanMove()
    {
        //raycast to find if theres any collider box in front of the player

        RaycastHit hit;
        Physics.Raycast(this.transform.position, -chick.transform.up, out hit, colliderDistCheck);
        Debug.DrawRay(this.transform.position, -chick.transform.up * colliderDistCheck, Color.red, 2);

        if( hit.collider == null)
        {
            SetMove();
        }
        else
        {
            if( hit.collider.tag == "Collider")
            {
                Debug.Log( "hit something with collider tag.");
            }
            else
            {
                SetMove();
            }
        }
    }


    void CheckIfCanMoveAngles()
    {
        RaycastHit hit;
        RaycastHit hitLeft;
        RaycastHit hitRight;

        Physics.Raycast(this.transform.position, -chick.transform.up, out hit, colliderDistCheck);
        Physics.Raycast(this.transform.position, -chick.transform.up + new Vector3(angleCheck, 0, 0), out hitLeft, colliderDistCheck + angleCheckDist);
        Physics.Raycast(this.transform.position, -chick.transform.up + new Vector3(-angleCheck, 0, 0), out hitRight, colliderDistCheck + angleCheckDist);

        Debug.DrawRay(this.transform.position, -chick.transform.up * colliderDistCheck, Color.red, 2);
        Debug.DrawRay(this.transform.position, (-chick.transform.up + new Vector3(-angleCheck, 0, 0) ) * colliderDistCheck, Color.green, 2);
        Debug.DrawRay(this.transform.position, (-chick.transform.up + new Vector3(angleCheck, 0, 0)) * colliderDistCheck, Color.blue, 2);

        if (hit.collider == null && hitLeft.collider == null && hitRight.collider == null)
        {
            SetMove();
        }
        else
        {
            if (hit.collider != null && hit.collider.tag == "Collider")
            {
                Debug.Log("hit something with collider tag.");
                isIdle = true;
            }
            else if(hitLeft.collider != null && hitLeft.collider.tag == "Collider")
            {
                Debug.Log("Hit something with left collider tag");
                isIdle = true;
            }
            else if (hitRight.collider != null && hitRight.collider.tag == "Collider")
            {
                Debug.Log("Hit something with right collider tag");
                isIdle = true;
            }

            else
            {
                SetMove();
            }
        }

    }

    void SetMove()
    {
        Debug.Log("Hit nothing. keep moving.");

        isIdle = false;
        isMoving = true;
        jumpStart = true;

    }

   

    void CanMove()
    {
        if (isMoving)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance));

                //if we actually are moving ahead we will increase score (necessary in case we went back so it doesnt count advancing again even we are not not further ahead than last time)
                if (transform.position.z + moveDistance > mostAdvancedPosition) { 
                    mostAdvancedPosition = transform.position.z + moveDistance;
                    SetMoveForwardState();
                }
                
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance));
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Moving(new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z));
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                Moving(new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z));
            }
        }

    }

    void Moving(Vector3 pos)
    {
        isIdle = false;
        isMoving = false;
        isJumping = true;
        jumpStart = false;

        PlayAudioClip(audioHop);
        //LeanTween.move(this.gameObject, pos, moveTime ).setOnComplete(MoveComplete);    
        StartCoroutine(Move(pos, moveTime));
    }

    IEnumerator Move(Vector3 pos, float moveTime)
    {
        Vector3 currentPos = transform.position;

        float t = 0;

        while(t < 1)
        {
            t += Time.deltaTime / moveTime;
            transform.position = Vector3.Lerp(currentPos, pos, t);
            yield return null;
        }

        Debug.Log("reached coroutine eding");
        MoveComplete();
    }

    void MoveComplete()
    {
        isJumping = false;
        isIdle = true;

        int a = Random.Range(0, 12);
        if (a < 4) PlayAudioClip(audioIdle2);
        
    }

    void SetMoveForwardState()
    {
        Manager.instance.UpdateDistanceCount();
    }

    void IsVisible()
    {
        if( myRenderer.isVisible)
        {
            isVisible = true;
        }

        if( !myRenderer.isVisible && isVisible )
        {
            Debug.Log("Player off screen. apply gothit()");
            GotHit();
        }
    }

    public void GotHit()
    {
        if (isDead)
            return;

        isDead = true;
        particle.Play();
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;
        
        PlayAudioClip(audioHit);

        Manager.instance.GameOver();

    }


    public void GotSoaked()
    {
        isDead = true;
        splash.Play();
        ParticleSystem.EmissionModule em = splash.emission;
        em.enabled = true;

        PlayAudioClip(audioSplash);

        chick.SetActive( false );

        Manager.instance.GameOver();

    }



    void PlayAudioClip(AudioClip clip)
    {
        this.GetComponent<AudioSource>().PlayOneShot(clip);
    }

}
