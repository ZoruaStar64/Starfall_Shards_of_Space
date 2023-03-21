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
    private Animator CosmoAnimator;
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
    private float antiCheckTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        state = "Idle";
        CosmoAnimator = GetComponent<Animator>();
        CosmoAnimator.SetInteger("State", 0);
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

        //If a player IsGrounded reset their walljump status so that they can walljump.
        if(IsGrounded())
        {
            hasWallJumped = false;
            wallJumpCount = 0;
        }

        //If a player is grounded after jumping count down the jumpTiming variable.
        //This is used for performing the double and triple jump.
        if (IsGrounded() && jumpTiming > 0f)
        {
            jumpTiming -= Time.deltaTime;
        }

        //If a player misses their window to perform a double or triple jump then reset their jump status back to the regular jump's values.
        if (IsGrounded() && jumpTiming <= 0f)
        {
            jumpHeight = 300;
            jumpCount = 0;
        }

        //Checks to see if player is 1: midair, 2: not facing a wall/wallsliding and 3: not groundpounding.
        /*if (!IsGrounded() && !FacingWall() && state != "GroundPounding")
        {
            CosmoAnimator.SetInteger("State", 2);
        }*/

        //Check to see if the player presses the spacebar while grounded and not crouching, if all the checks pass then make the player jump.
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !IsCrouching())
        {
            antiCheckTimer = 1;
            Jump();
        }

        //if a player is midair and then presses the leftshift key then set the doGroundPound variable to true.
        if (!IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                doGroundPound = true;
            }
        }

        //While the player state is GroundPounding check if the player becomes grounded,
        //then set the doGroundPound variable to false and their state to Idle.
        if (state == "GroundPounding")
        {
            if (IsGrounded())
            {
                doGroundPound = false;
                //rb.useGravity = true;
                state = "Idle";
            }
        }

        //This will set the player's animation state to Wallshuffling/holding (dunno a good name yet)
        if (FacingWall() && IsGrounded())
        {
            CosmoAnimator.SetInteger("State", 11);
        }

        //if the player is facing a wall, they are not grounded AND have no more wallSlideImmunity
        //make them WallSlide while also setting their animation state to wallsliding
        if (FacingWall() && !IsGrounded() && wallSlideImmunity <= 0)
        {
            CosmoAnimator.SetInteger("State", 12);
            WallSlide();
        } 

        //if a player presses the spacebar, is facing a wall, is not grounded and hasn't walljumped
        //make them walljump while setting their animation state to walljumping.
        if (Input.GetKeyDown(KeyCode.Space) && FacingWall() && !IsGrounded() && hasWallJumped == false)
        {
            CosmoAnimator.SetInteger("State", 13);
            WallJump();
        }

        //if the wallSlideImmunity is above 0 (done by jumping against a wall).
        //make the wallSlideImmunity variable countdown.
        if (wallSlideImmunity > 0f)
        {
            wallSlideImmunity -= Time.deltaTime;
        }

        if (antiCheckTimer > 0f)
        {
            antiCheckTimer -= 0.1f;
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
            CosmoAnimator.SetInteger("State", 0);
        }

        if (moveDirection != Vector3.zero && IsGrounded() && !IsCrouching() && walkSpeed > 0)
        {
            state = "Moving";
            CosmoAnimator.SetInteger("State", 1);
            canCrouchSlide = true;
        }

        if (IsCrouching() && moveDirection == Vector3.zero)
        {
            state = "Crouching";
            CosmoAnimator.SetInteger("State", 6);
            canCrouchSlide = false;
            ChangeColliderSize(1.3f, -0.46f);
            //reduce player collisionbox height by 50% or more
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsCrouching() && moveDirection == Vector3.zero)
        {
            CosmoAnimator.SetInteger("State", 7);
            antiCheckTimer = 1;
            Backflip();
        }

        if (IsCrouching() && moveDirection != Vector3.zero && canCrouchSlide)
        {
            //CosmoAnimator.SetInteger("State", 6);
            CrouchSlide();
            ChangeColliderSize(1.3f, -0.46f);
        }

        if (IsCrouching() && moveDirection != Vector3.zero && !canCrouchSlide)
        {
            walkSpeed = 25;
            CosmoAnimator.SetInteger("State", 10);
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
        if (antiCheckTimer <= 0)
        {
            Vector3 downwards = transform.TransformDirection(Vector3.down);
            return Physics.Raycast(transform.position, downwards, distToGround + 0.1f);
        }
        return false;
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
            CosmoAnimator.SetInteger("State", 3);
            JumpSound.pitch = 1;
            JumpSound.Play();
        }
        if (jumpCount == 2)
        {
            state = "DoubleJump";
            CosmoAnimator.SetInteger("State", 4);
            JumpSound.pitch = 1.25f;
            JumpSound.Play();
        }
        if (jumpCount == 3)
        {
            state = "TripleJump";
            CosmoAnimator.SetInteger("State", 5);
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
        CosmoAnimator.SetInteger("State", 14);
        //rb.useGravity = false;
    }

    IEnumerator PoundDown()
    {
        yield return new WaitForSeconds(0.5f);
        CosmoAnimator.SetInteger("State", 15);
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
            //CosmoAnimator.PlayInFixedTime("State", 6, 0.0f);
            walkSpeed = walkSpeed - 1;
        }
        if (walkSpeed > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            antiCheckTimer = 1;
            CosmoAnimator.SetInteger("State", 9);
            LongJump();
            canCrouchSlide = false;
        }
    }

    void Crawl()
    {
        state = "Crawling";
        walkSpeed = walkSpeed / 1.5f;
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
