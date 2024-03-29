using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    #region COMPONENTS
    private Rigidbody2D rb_;
    [SerializeField] private GameObject RotatingObject;
    #endregion

    #region STATE PARAMETERS
    private bool isGrounded_;
    private bool isOnRightWall_;
    private bool isOnLeftWall_;
    private bool isJumping_;

    //private bool isWallJumping_;
    //private int lastWallJumpDir_;
    //private float wallJumpStartTime_;
    //public bool useWallJump = true;

    public float lastOnGroundTime { get; private set; }
    public float lastOnWallTime { get; private set; }
    public float lastOnWallRightTime { get; private set; }
    public float lastOnWallLeftTime { get; private set; }
    float footStepsDelay = 0.35f;
    #endregion


    #region INPUT PARAMETERS
    private float moveInput_;
    public float lastPressedJumpTime { get; private set; }
    #endregion

    #region CHECK PARAMETERS
    [Header("Checks")]
    [SerializeField] private Transform groundCheckPoint_;
    [SerializeField] private Vector2 groundCheckSize_;
    [Space(5)]
    [SerializeField] private Transform frontWallCheckPoint_;
    [SerializeField] private Transform backWallCheckPoint_;
    [SerializeField] private Vector2 wallCheckSize_;
    #endregion

    #region Layers & Tags
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask groundLayer_;
    #endregion

    #region Particle
    //public ParticleSystem footsteps;
    //public ParticleSystem impactEffect;
    private bool wasOnGround = true;
    //private ParticleSystem.EmissionModule footEmission;
    #endregion

    [SerializeField] private float moveSpeed_;
    [SerializeField] private float velPower_;

    [SerializeField] private float acceleration_;
    [SerializeField] private float deccelaration_;
    [SerializeField] private float fallClamp_;

    [Range(0, 1)] public float jumpCutMult;

    [Range(0, 0.5f)] public float coyoteTime;
    [Range(0, 1.5f)] public float jumpBufferTime;

    public float fallGravityMult;
    public float gravityScale;
    public float frictionAmount;
    public float jumpForce_;

    [System.NonSerialized] public bool top_;
    [System.NonSerialized] public bool isFacingRight;

    [SerializeField] private GameObject buttonImageGameObject;

    private void Awake()
    {
        if (rb_ == null)
            rb_ = GetComponent<Rigidbody2D>();

        if (instance == null)
        {
            instance = this;
        }

        buttonImageGameObject.SetActive(false);
    }

    private void Start()
    {
        isFacingRight = true;
    }

    private void Update()
    {
        #region TIMERS
        lastOnGroundTime -= Time.deltaTime;
        lastOnWallTime -= Time.deltaTime;
        lastOnWallRightTime -= Time.deltaTime;
        lastOnWallLeftTime -= Time.deltaTime;

        lastPressedJumpTime -= Time.deltaTime;
        #endregion

        moveInput_ = Input.GetAxisRaw("Horizontal");

        #region GENERAL CHECKS
        if (moveInput_ != 0)
        {
            CheckDirectionToFace(moveInput_ > 0.01f);
            if (!isJumping_ && isGrounded_)
            {
                footStepsDelay -= Time.deltaTime;
                if (footStepsDelay < 0f)
                {
                    SoundManager.PlaySound(SoundManager.SoundType.PlayerFootsteps, 0.45f);
                }
            }
        }
        #endregion

        #region PHYSICS CHECKS
        //Ground Check
        if (Physics2D.OverlapBox(groundCheckPoint_.position, groundCheckSize_, 0, groundLayer_))
        {
            isGrounded_ = true;
        }
        else
        {
            isGrounded_ = false;
        }
        //Right Wall Check
        if ((Physics2D.OverlapBox(frontWallCheckPoint_.position, wallCheckSize_, 0, groundLayer_) && isFacingRight)
                || (Physics2D.OverlapBox(backWallCheckPoint_.position, wallCheckSize_, 0, groundLayer_) && !isFacingRight))
        {
            isOnRightWall_ = true;
        }
        else
        {
            isOnRightWall_ = false;
        }
        //Left Wall Check
        if ((Physics2D.OverlapBox(frontWallCheckPoint_.position, wallCheckSize_, 0, groundLayer_) && !isFacingRight)
            || (Physics2D.OverlapBox(backWallCheckPoint_.position, wallCheckSize_, 0, groundLayer_) && isFacingRight))
        {
            isOnLeftWall_ = true;
        }
        else
        {
            isOnLeftWall_ = false;
        }

        if (!isJumping_)
        {
            if (isGrounded_)
            {
                lastOnGroundTime = coyoteTime;
            }
            if (isOnRightWall_)
            {
                lastOnWallRightTime = coyoteTime;
            }
            if (isOnLeftWall_)
            {
                lastOnWallLeftTime = coyoteTime;
            }
        }
        #endregion

        #region Gravity
        if (!top_)
        {
            if (rb_.velocity.y < 0)
            {
                rb_.gravityScale = gravityScale * fallGravityMult;
            }
            else
            {
                rb_.gravityScale = gravityScale;
            }
        }
        else
        {
            if (rb_.velocity.y > 0)
            {
                rb_.gravityScale = gravityScale * fallGravityMult;
            }
            else
            {
                rb_.gravityScale = gravityScale;
            }
        }
        if (!top_)
        {
            if (rb_.velocity.y < 0 && rb_.velocity.y < fallClamp_)
            {
                rb_.velocity = new Vector2(rb_.velocity.x, fallClamp_);
            }
        }
        else
        {
            if (rb_.velocity.y > 0 && rb_.velocity.y > -fallClamp_)
            {
                rb_.velocity = new Vector2(rb_.velocity.x, -fallClamp_);
            }
        }
        #endregion

        #region JUMP
        OnJump();

        if (!top_)
        {
            if (rb_.velocity.y < 0 && isJumping_)
            {
                isJumping_ = false;
            }
        }
        else
        {
            if (rb_.velocity.y > 0 && isJumping_)
            {
                isJumping_ = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            isJumping_ = true;
            Jump();
        }

        if (CanJump() && lastPressedJumpTime > 0)
        {
            isJumping_ = true;
            Jump();
        }
        //Jump Cut
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!top_)
            {
                OnJumpUp();
            }
            else
            {
                OnJumpDown();
            }
        }
        #endregion

        if (lastOnGroundTime > 0 && Mathf.Abs(moveInput_) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb_.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb_.velocity.x);
            rb_.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
            footStepsDelay = 0.35f;
        }

        #region Particle
        //if (moveInput_ != 0 && isGrounded_)
        //{
        //    footEmission.rateOverTime = 20f;
        //}
        //else
        //{
        //    footEmission.rateOverTime = 0;
        //}

        //if (!wasOnGround && isGrounded_)
        //{
        //    impactEffect.gameObject.SetActive(true);
        //    impactEffect.Stop();
        //    impactEffect.transform.position = footsteps.transform.position;
        //    impactEffect.Play();
        //}

        if (!wasOnGround && isGrounded_)
        {
            SoundManager.PlaySound(SoundManager.SoundType.JumpLanding);
        }
        wasOnGround = isGrounded_;
    }
    #endregion

    private void FixedUpdate()
    {
        #region INPUT HANDLER
        Run();
        #endregion
    }

    private void Run()
    {
        float targetSpeed = moveInput_ * moveSpeed_;
        float speedDiff = targetSpeed - rb_.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration_ : deccelaration_;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower_) * Mathf.Sign(speedDiff);
        rb_.AddForce(movement * Vector2.right);
    }

    private void Jump()
    {
        lastPressedJumpTime = 0;
        lastOnGroundTime = 0;

        #region Perform Jump
        float force = jumpForce_;
        if (!top_)
        {
            if (rb_.velocity.y < 0)
                force -= rb_.velocity.y;
        }
        else
        {
            if (rb_.velocity.y > 0)
                force += rb_.velocity.y;
        }
        if (!top_)
        {
            rb_.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        else
        {
            rb_.AddForce(Vector2.down * force, ForceMode2D.Impulse);
        }

        #endregion
    }

    private bool CanJump()
    {
        return lastOnGroundTime > 0 && !isJumping_;
    }

    public void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lastPressedJumpTime = jumpBufferTime;
        }
    }

    public void OnJumpUp()
    {
        if (CanJumpCut() && rb_.velocity.y > 0)
        {
            JumpCut();
        }
    }
    public void OnJumpDown()
    {
        if (CanJumpCut() && rb_.velocity.y < 0)
        {
            JumpCut();
        }
    }

    private bool CanJumpCut()
    {
        return isJumping_;
    }

    private void JumpCut()
    {
        rb_.AddForce(Vector2.down * rb_.velocity * (1 - jumpCutMult), ForceMode2D.Impulse);
    }

    private void Turn()
    {
        Vector3 scale = RotatingObject.transform.localScale;
        scale.x *= -1;
        RotatingObject.transform.localScale = scale;

        isFacingRight = !isFacingRight;
    }

    public GameObject GetButtonGameObject()
    {
        return buttonImageGameObject;
    }

    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != isFacingRight)
            Turn();
    }
    #endregion
}