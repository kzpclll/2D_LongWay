using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

public class PlayerKid : PlayerBase
{
    [Header("普通 最大移动速度")]
    public float maxRunSpd;
    public float jumpPow;
    [Header("滑动参数")]
    public float slidJumpPow;
    [Header("比这个速度小则大幅加速")]
    public float minSlidSpd;
    [Header("最快不会超过这个速度")]
    public float maxSlidSpd;
    [Header("Debug")]
    public float spd;

    //[Header("控制参数")]
    private readonly float accTime = 0.65f;
    private readonly float decTime = 0.3f;
    private readonly float airMult = 0.65f;

    //[Header("计算参数")]
    private float accPerTime;
    private float decPerTime;

    //[Header("状态参数")]
    private float xVelocity;
    private bool isNearGround;
    private bool isJumpPressed;
    private bool isJumping;


    [HideInInspector]
    public bool isUp;
    [HideInInspector]
    public bool isDown;

    public float detachmentForce = 10f;

    public enum PlayMode
    {
        normal,
        slid,
        animation
    }
    public PlayMode pm;

    //土狼时间、lazyJumpTimer


    //组件
    [HideInInspector]public Animator playerAnimator;

    public SavePoint savePoint;
    public Leader dad;
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    private PlayMode savedPlaymode;
    private Quaternion playerRotation;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponentInChildren<Animator>();
        accPerTime = maxRunSpd / accTime;
        decPerTime = maxRunSpd / decTime;
        playerRotation = transform.rotation;
        groundCheck = transform.Find("GroundCheck");
    }


    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            ProcessSpacebarPress();
            isJumpPressed = true;

        }
        isNearGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        playerAnimator.SetBool("isNearGround", isNearGround);

        // playerMode
        if (pm == PlayMode.normal)
        {
            xVelocity = Input.GetAxisRaw("Horizontal");
            PlayerMovement();
            Jump(jumpPow);
        }
        else if (pm == PlayMode.slid)
        {
            xVelocity = 1;
            Slide();
            GravityControl();
            if (isJumpPressed)
            {
                SlideJump();
            }
        }
        else if (pm == PlayMode.animation)
        {
            // do nothing
        }
        else
        {
            // do nothing
        }
        if (isNearGround && pm == PlayMode.animation)
        {
            BeginSlid();
        }

        StatusCtr();

    }

    // 角色移动与动画 pm = normal
    private void PlayerMovement()
    {
        float xVelocityAbs = Mathf.Abs(xVelocity);

        //movement
        float mult = isNearGround ? 1 : airMult;

        if (xVelocityAbs > 0)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, xVelocity * maxRunSpd, accPerTime * mult * Time.deltaTime), rb.velocity.y);
        }
        else if (xVelocity == 0)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, xVelocity * maxRunSpd, decPerTime * mult * Time.deltaTime), rb.velocity.y);
        }
        else
        {
            Debug.LogError("error xVelocityAbs =" + xVelocityAbs);
        }

        float velocityXAbs = Mathf.Abs(rb.velocity.x);

        //animator - run
        if (xVelocityAbs > 0)
        {
            playerAnimator.SetFloat("xVelocity", xVelocityAbs);
            playerAnimator.speed = 1.2f - ((maxRunSpd - velocityXAbs) / maxRunSpd);
        }
        else if (xVelocity == 0)
        {
            playerAnimator.speed = 1;
            playerAnimator.SetFloat("xVelocity", xVelocityAbs);
        }
        else
        {
            Debug.LogError("error xVelocityAbs =" + xVelocityAbs);
        }

        if (xVelocity != 0)
        {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(xVelocity * Mathf.Abs(scale.x), scale.y, 1);
        }
    }

    // jump pm = normal
    private void Jump(float jumpPow)
    {
        if (isJumpPressed && !isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpPow);
            if (xVelocity != 0)
            {
                rb.velocity += new Vector2(xVelocity * 0.5f, 0);
            }
        }
        isJumpPressed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // 取消跳跃状态的标记
        }
    }


    // update pm = slide
    private void Slide()
    {
        //Slid
        float mult = isNearGround ? 0.1f : 0.1f;
        Vector2 temp = rb.velocity;
        if (temp.x < minSlidSpd)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(temp.x, maxSlidSpd, 4 * accPerTime * mult * Time.deltaTime), temp.y);

        }
        else if (rb.velocity.x > maxSlidSpd)
        {
            rb.velocity = new Vector2(maxSlidSpd, temp.y);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(temp.x, maxSlidSpd, 2 * accPerTime * mult * Time.deltaTime), temp.y);
        }
        spd = rb.velocity.x;
    }


    //update pm = slide
    private void SlideJump()
    {
        if (isJumpPressed && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(0, rb.velocity.y) + jumpPow);
            isJumping = true;
        }
        isJumpPressed = false;
    }

    public void ProcessSpacebarPress()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rb.transform.parent != null)
        {
            Vector2 contactPoint = rb.transform.position;
            Vector2 tangentDirection = -rb.transform.up;
            rb.GetComponent<Rigidbody2D>().velocity = tangentDirection * detachmentForce;
            rb.transform.SetParent(null);

        }
    }

    // magicfunction 控制重力 pm = slide
    private void GravityControl()
    {
        if (!isNearGround)
        {
            if (isUp && !isDown)
            {
                rb.gravityScale = 1f;
            }
            else if (isDown && !isUp)
            {
                rb.gravityScale = 1f;
            }
            else
            {
                rb.gravityScale = 1f;
            }
        }
        else
        {
            if (isUp && !isDown)
            {
                rb.gravityScale = 0.1f;
            }
            else if (isDown && !isUp)
            {
                rb.gravityScale = 0.1f;
            }
            else
            {
                rb.gravityScale = 0.1f;
            }
        }

    }


    // 尝试控制重力与动画
    private void StatusCtr()
    {
        if (rb.velocity.y > 0)
        {

            isDown = false;
            isUp = true;
            playerAnimator.SetBool("isUp", true);
            playerAnimator.SetBool("isDown", false);
        }
        else if (rb.velocity.y < 0)
        {
            isUp = false;
            isDown = true;
            playerAnimator.SetBool("isUp", false);
            playerAnimator.SetBool("isDown", true);
        }
        else
        {
            isUp = false;
            isDown = false;
            playerAnimator.SetBool("isUp", false);
            playerAnimator.SetBool("isDown", false);
        }

        if (isNearGround)
        {
            playerAnimator.SetBool("isUp", false);
            playerAnimator.SetBool("isDown", false);
        }
    }


    public void BeginSlid()
    {
        playerAnimator.SetBool("isSlid", true);
        pm = PlayMode.slid;
    }

    public void EndSlid()
    {
        playerAnimator.SetBool("isSlid", false);
        pm = PlayMode.normal;
    }
    public void DoNothing()
    {
        pm = PlayMode.animation;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Follower"))
        {
            other.GetComponent<Follower>().WaitActive();
        }
        if (other.CompareTag("SavePoint"))
        {
            savePoint = other.gameObject.GetComponent<SavePoint>();
            savePoint.UpdateSavePoint(GetComponent<PlayerKid>());

        }
       
    }
    public void Re()
    {
        transform.parent = null;
        transform.rotation = playerRotation;
        rb.velocity = Vector2.zero;
        pm = PlayMode.normal;
        GetComponent<Rigidbody2D>().gravityScale = 1.3f;
        EndSlid();
        playerAnimator.Play("待机");
        dad.Reset();
    }


}
