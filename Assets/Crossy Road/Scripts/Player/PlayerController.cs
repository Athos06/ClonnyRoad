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

    public float offSetRightRaycast;
    public float offSetLeftRaycast;


    public PlayerInput playerInput;

    private Vector3 movementDirection;
    public Vector3 MovementDirection
    {
        get
        {
            return movementDirection;
        }
        set
        {
            movementDirection = value;
        }
    }

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

    }

    void CanIdle()
    {
        if (isIdle)
        {
            foreach(Command inputCommand in playerInput.GetInput())
            {
                inputCommand.Execute(this);
            }
        }
    }



    public void CheckIfIdle(Quaternion rot)
    {
        chick.transform.rotation = rot;

        CheckIfCanMove();

        int a = Random.Range(0, 12);
        if (a < 4) PlayAudioClip(audioIdle1);
    }


    void CheckIfCanMove()
    {

        RaycastHit hit;
        RaycastHit hitLeft;
        RaycastHit hitRight;

        Vector3 rightPos = new Vector3(this.transform.position.x + offSetRightRaycast, this.transform.position.y, this.transform.position.z);
        Vector3 leftPos = new Vector3(this.transform.position.x + offSetLeftRaycast, this.transform.position.y, this.transform.position.z);

        Physics.Raycast(this.transform.position, -chick.transform.up, out hit, colliderDistCheck);
        Physics.Raycast(rightPos, -chick.transform.up, out hitLeft, colliderDistCheck + angleCheckDist);
        Physics.Raycast(leftPos, -chick.transform.up, out hitRight, colliderDistCheck + angleCheckDist);

        Debug.DrawRay(this.transform.position, -chick.transform.up * colliderDistCheck, Color.red, 2);
        Debug.DrawRay(rightPos, -chick.transform.up * colliderDistCheck, Color.green, 2);
        Debug.DrawRay(leftPos, -chick.transform.up * colliderDistCheck, Color.blue, 2);
        

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
            else if (hitLeft.collider != null && hitLeft.collider.tag == "Collider")
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
        isIdle = false;
        isMoving = true;
        jumpStart = true;

    }

    void CanMove()
    {
        if (isMoving)
        {
            if (movementDirection == Vector3.forward)
            {
                if (transform.position.z + moveDistance > mostAdvancedPosition)
                {
                    mostAdvancedPosition = transform.position.z + moveDistance;
                    SetMoveForwardState();
                }

                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance));

            }
            else if (movementDirection == Vector3.back)
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance));
            }
            else if (movementDirection == Vector3.left)
            {
                Moving(new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z));
            }
            else if (movementDirection == Vector3.right)
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


        PlayAudioClip(audioHop);
        StartCoroutine(StartMovement(pos, moveTime));
    }

    IEnumerator StartMovement(Vector3 pos, float moveTime)
    {
        //0.06 is a magic number, basically the time we take to get out of the start jumping animation, because what we want is first do that little animation and then starts the movement
        //TODO change magic number for something more understandable
        yield return new WaitForSeconds(0.06f);
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

        MoveComplete();
    }

    void MoveComplete()
    {
        isJumping = false;
        jumpStart = false;
        isIdle = true;

        int a = Random.Range(0, 12);
        if (a < 4) PlayAudioClip(audioIdle2);
        
    }

    void SetMoveForwardState()
    {
        Manager.instance.UpdateDistanceCount();
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

    public void GotOutOfBounds()
    {
        isDead = true;

        PlayAudioClip(audioHit);

        chick.SetActive(false);

        Manager.instance.GameOver();


    }
    
    void PlayAudioClip(AudioClip clip)
    {
        this.GetComponent<AudioSource>().PlayOneShot(clip);
    }

}
