using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Related Objects")]
    public GameObject RotationPoint;
    public Transform rotatePoint;
    public GameObject GameCamera;
    public Rigidbody rb;
    BoxCollider BC;
    [Header("Movement")]
    public string state;
    public float walkSpeed;
    Vector3 moveDirection;
    public bool canCrouchSlide;
    [Header("Jumping")]
    public float jumpHeight;
    public int jumpCount;
    public float jumpTiming;
    private bool doGroundPound = false;
    AudioSource JumpSound;
    [Header("Wall Jumping")]
    public float distToWall;
    public bool hasWallJumped;
    public int wallJumpCount;
    public float wallSlideImmunity;
    [Header("Groundedness")]
    //RaycastHit m_Hit;
    public float distToGround;

    // Start is called before the first frame update
    void Start()
    {
        state = "Idle";
        JumpSound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (doGroundPound)
        {
            GroundPound();
        }
        if (state != "GroundPounding")
        {
            Movement();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + (Vector3)rb.velocity);

        // Vector3.forward* 

        //rb.velocity = new Vector3((Time.deltaTime * verticalInput * walkSpeed), rb.velocity.y,(-horizontalInput * Time.deltaTime * walkSpeed));

        if(IsGrounded())
        {
            hasWallJumped = false;
            wallJumpCount = 0;
        }

        if (IsGrounded() && jumpTiming > 0f)
        {
            jumpTiming -= Time.deltaTime;
        }

        if (IsGrounded() && jumpTiming <= 0f)
        {
            jumpHeight = 300;
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !IsCrouching())
        {
            Jump();
        }

        if (!IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                doGroundPound = true;
            }
        }

        if (state == "GroundPounding")
        {
            if (IsGrounded())
            {
                doGroundPound = false;
                //rb.useGravity = true;
                state = "Idle";
            }
        }

        if (FacingWall() && !IsGrounded() && wallSlideImmunity <= 0)
        {
            WallSlide();
        } 

        if (Input.GetKeyDown(KeyCode.Space) && FacingWall() && !IsGrounded() && hasWallJumped == false)
        {
            WallJump();
        }

        if (wallSlideImmunity > 0f)
        {
            wallSlideImmunity -= Time.deltaTime;
        }
    }

    /* Movement functions */

    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        BC = GetComponent<BoxCollider>();

        if (!IsCrouching() || !IsGrounded())
        {
            walkSpeed = 25;
            ChangeColliderSize(2.2f, 0);
        }

        moveDirection = new Vector3(horizontalInput, 0f, verticalInput);

        if (moveDirection != Vector3.zero && IsGrounded())
        {
            RotateChar();
        }

        if (moveDirection == Vector3.zero && IsGrounded() && !IsCrouching())
        {
            state = "Idle";
        }

        if (moveDirection != Vector3.zero && IsGrounded() && !IsCrouching() && walkSpeed > 0)
        {
            state = "Moving";
            canCrouchSlide = true;
        }

        if (IsCrouching() && moveDirection == Vector3.zero)
        {
            state = "Crouching";
            canCrouchSlide = false;
            ChangeColliderSize(1.1f, -0.55f);
            //reduce player collisionbox height by 50% or more
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsCrouching() && moveDirection == Vector3.zero)
        {
            Backflip();
        }

        if (IsCrouching() && moveDirection != Vector3.zero && canCrouchSlide)
        {
            CrouchSlide();
            ChangeColliderSize(1.1f, -0.55f);
        }

        if (IsCrouching() && moveDirection != Vector3.zero && !canCrouchSlide)
        {
            walkSpeed = 25;
            Crawl();
            ChangeColliderSize(0.9f, -0.65f);
        }

        if (moveDirection != Vector3.zero && hasWallJumped == true)
        {
            hasWallJumped = false;
        }

        moveDirection = transform.rotation * moveDirection;
        rb.AddForce(moveDirection.normalized * walkSpeed, ForceMode.Force);
    }

    void RotateChar()
    {
        Vector3 CameraRotation = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);

        transform.eulerAngles = new Vector3(0, CameraRotation.y, 0);
    }

    bool IsGrounded()
    {
        Vector3 downwards = transform.TransformDirection(Vector3.down);
        return Physics.Raycast(transform.position, downwards, distToGround + 0.1f);
    }    

    /* Regular jump/groundpound functions */

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight);
        if (jumpCount < 3 && jumpTiming > 0f || jumpCount == 0)
        {
            jumpCount += 1;
            jumpHeight += 100;
        }
        if (jumpCount == 1)
        {
            state = "RegularJump";
            JumpSound.pitch = 1;
            JumpSound.Play();
        }
        if (jumpCount == 2)
        {
            state = "DoubleJump";
            JumpSound.pitch = 1.25f;
            JumpSound.Play();
        }
        if (jumpCount == 3)
        {
            state = "TripleJump";
            JumpSound.pitch = 1.5f;
            JumpSound.Play();
            jumpHeight = 300;
            jumpCount = 0;
        }
        jumpTiming = 1f;
    }  

    void GroundPound()
    {
        state = "GroundPounding";
        StopAndSpin();
        StartCoroutine("PoundDown");

    }

    void StopAndSpin()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //rb.useGravity = false;
    }

    IEnumerator PoundDown()
    {
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(Vector3.down * 16f, ForceMode.Impulse);
    }
    
    /* Wall related functions/bool */

    bool FacingWall()
    {
        Vector3 forwards = transform.TransformDirection(Vector3.forward);
        return Physics.Raycast(transform.position, forwards, distToWall + 0.1f);
    }

    void WallSlide()
    {
        state = "WallSliding";
        rb.AddForce(Vector3.down * 1.2f);
    }

    void WallJump()
    {
        state = "WallJumping";
        rb.AddForce(Vector3.up * 500);
        hasWallJumped = true;
        transform.eulerAngles = new Vector3(0, rb.transform.eulerAngles.y + 180, 0);
        JumpSound.pitch = 1;
        JumpSound.Play();
        wallSlideImmunity = 1.0f;
    }

    /* Crouching functions/bool */

    bool IsCrouching()
    {
        if (Input.GetKey(KeyCode.LeftShift) && IsGrounded())
        {
            return true;
            //When LeftShift is held while grounded make collision about half or less normal size
        }
        return false;
    }

    void ChangeColliderSize(float NewHeight, float NewPosition)
    {
        BC.size = new Vector3(BC.size.x, NewHeight, BC.size.z);
        BC.center = new Vector3(0, NewPosition, 0);
    }

    void CrouchSlide()
    {
        //will probably be replace with a slide/sliding kick
        state = "Sliding";
        if (walkSpeed <= 0)
        {
            walkSpeed = 0;
            canCrouchSlide = false;
        }
        if (walkSpeed > 0)
        {
            walkSpeed = walkSpeed - 1;
        }
        if (walkSpeed > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            LongJump();
            canCrouchSlide = false;
        }
    }

    void Crawl()
    {
        state = "Crawling";
        walkSpeed = walkSpeed / 2;
        //if crouching then lower base speed by half or 2/3 of normal speed
        //Potentially add a walking mechanic? which would just be walkspeed divided by 1.5f
    }

    void Backflip()
    {
        state = "Backflipping";
        JumpSound.pitch = 0.8f;
        JumpSound.Play();
        rb.AddForce(Vector3.up * 850);
        rb.AddForce(transform.rotation * Vector3.back * 150);
    }

    void LongJump()
    {
        state = "LongJumping";
        JumpSound.pitch = 1.25f;
        JumpSound.Play();
        rb.AddForce(Vector3.up * 250);
        rb.AddForce(transform.rotation * Vector3.forward * 400);
        //This will do a longjump like in the 3d mario games
    }
}
