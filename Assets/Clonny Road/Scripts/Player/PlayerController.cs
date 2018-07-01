using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    [Header("Movement properties")]
    [SerializeField]
    private float moveDistance = 1;
    [SerializeField]
    private float moveTime = 0.4f;
    [SerializeField]
    private float colliderDistCheck = 1;

    [Header("Player State flags")]
    public bool isIdle = true;
    public bool isDead = false;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool jumpStart = false;
    public bool parentedToObject = false;

    public ParticleSystem particle = null;
    public ParticleSystem splash = null;
    public GameObject chick = null;

    [Header("Audio")]
    public AudioClip audioIdle1 = null;
    public AudioClip audioIdle2 = null;
    public AudioClip audioHop = null;
    public AudioClip audioHit = null;
    public AudioClip audioSplash = null;
    
    private float mostAdvancedPosition;

    [SerializeField]
    private float offSetRightRaycast;
    [SerializeField]
    private float offSetLeftRaycast;

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

    private Vector3 movementVector;

    public PlayerInput playerInput;

    private void Start()
    {
        mostAdvancedPosition = transform.position.z;
    }

    private void Update()
    {
        if (!Manager.Instance.CanPlay()) return;
        if (isDead) return;

        UpdateMovement();
        CanMove();

    }

    /// <summary>
    /// Called everyframe to get the input from the player
    /// </summary>
    void UpdateMovement()
    {
        if (isIdle)
        {
            //We get the player input for this frame
            foreach(Command inputCommand in playerInput.GetInput())
            {
                inputCommand.Execute(this);
            }
        }
    }


    /// <summary>
    /// We apply the rotation and try to apply the movement if its possible
    /// </summary>
    /// <param name="rot"></param>
    public void SetRotationAndMovement(Quaternion rot)
    {
        //we apply rotation
        chick.transform.rotation = rot;

        if (CheckIfCanMove())
        {
            //if we can move we start the movement
            SetMove();
        }
        else
        {
            //otherwise we are idle
            isIdle = true;
        }

        int a = Random.Range(0, 12);
        if (a < 4) PlayAudioClip(audioIdle1);
    }

    bool CheckIfCanMove()
    {
        RaycastHit hit;
        RaycastHit hitLeft;
        RaycastHit hitRight;

        Vector3 rightPos = new Vector3(this.transform.position.x + offSetRightRaycast, this.transform.position.y, this.transform.position.z);
        Vector3 leftPos = new Vector3(this.transform.position.x + offSetLeftRaycast, this.transform.position.y, this.transform.position.z);

        Physics.Raycast(this.transform.position, -chick.transform.up, out hit, colliderDistCheck);
        Physics.Raycast(rightPos, -chick.transform.up, out hitLeft, colliderDistCheck);
        Physics.Raycast(leftPos, -chick.transform.up, out hitRight, colliderDistCheck);

        Debug.DrawRay(this.transform.position, -chick.transform.up * colliderDistCheck, Color.red, 2);
        Debug.DrawRay(rightPos, -chick.transform.up * colliderDistCheck, Color.green, 2);
        Debug.DrawRay(leftPos, -chick.transform.up * colliderDistCheck, Color.blue, 2);
        
        if (hit.collider != null && hit.collider.tag == "Collider")
        {
            return false;
        }
        else if (hitLeft.collider != null && hitLeft.collider.tag == "Collider")
        {
            return false;
        }
        else if (hitRight.collider != null && hitRight.collider.tag == "Collider")
        {
            return false;
        }
        else
        {
            return true;
        }
            
    }


    /// <summary>
    /// Sets the flags to start the movement
    /// </summary>
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

    void Moving(Vector3 movementVec)
    {
        isIdle = false;
        isMoving = false;
        isJumping = true;

        PlayAudioClip(audioHop);

        movementVector = movementVec;
    }
    
    /// <summary>
    /// Will be called by the AnimController when the startJumping animation finishes and call the event
    /// </summary>
    public void StartMovement()
    {
        StartCoroutine(Move(movementVector, moveTime));
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

        PlayRandomSound();

    }

    private void PlayRandomSound()
    {
        int a = Random.Range(0, 12);
        if (a < 4) PlayAudioClip(audioIdle2);
    }

    void SetMoveForwardState()
    {
        Manager.Instance.UpdateDistanceCount();
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

        Manager.Instance.GameOver();

    }

    public void GotSoaked()
    {
        isDead = true;
        splash.Play();
        ParticleSystem.EmissionModule em = splash.emission;
        em.enabled = true;

        PlayAudioClip(audioSplash);

        chick.SetActive( false );

        Manager.Instance.GameOver();

    }

    public void GotOutOfBounds()
    {
        isDead = true;

        PlayAudioClip(audioHit);

        chick.SetActive(false);

        Manager.Instance.GameOver();


    }
    
    void PlayAudioClip(AudioClip clip)
    {
        this.GetComponent<AudioSource>().PlayOneShot(clip);
    }

}
