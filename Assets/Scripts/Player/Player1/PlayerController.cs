using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerBase
{
    #region
    // 主要组件和Layer初始化
    
    public Animator playerAnimator;
    private BoxCollider2D bc2d;
    private CapsuleCollider2D cc2d;
    public SpriteRenderer sr;
    public LayerMask ground;
    public Transform groundCheck;

    // 移动参数
    [Header("移动参数")]
    public float speed;
    public float acc;
    public float accStop;
    public float accSpace;
    public float accSpaceStop;

    // 跳跃参数
    [Header("跳跃参数")]
    public int jumpMaxTime = 1;
    public int jumpTime;
    public float jumpVelocity = 6.5f;
    public float graceFrame = 6;
    private float restGraceFrame;

    [Header("水下移动参数")]
    public int MAXSPDx;
    public int MAXSPDy;
    public int accWater;
    [HideInInspector]
    // 跳跃、互动检测
    private bool isNearGround;

    // 状态控制

    // playerState控制Jump按键事件 1跳跃 2互动
    // isPlayerMove 是否接受操作
    [Header("状态参数")]
    public bool isRush;

    [Header("冲刺参数")]
    private Vector3 aimPosition;
    private float rushSpeed;

    //变量
    public float xVelocity;
    public float yVelocity;
    public Vector2 checkPointPosition;

    private Vector2 CapsuleColiderSize;

    #endregion
    void Start()
    {
        //获取rigidbody，animation，colider
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();
        cc2d = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        jumpTime = jumpMaxTime;
        restGraceFrame = 0;

        playerState = 1;

        initPlayScene(playScene);

        CapsuleColiderSize = cc2d.size;

    }

    public float coolDownTime;
    void Update()
    {
        //接收Jump按键
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;

            
            {
                coolDownTime = 0.5f;
            }
        }

        //土狼时间 起跳、站地、落下
        if(rb.velocity.y>0.01f && isNearGround)
        {
            restGraceFrame = 0;
        }

        if (isNearGround)
        {
            restGraceFrame = graceFrame;
        }
        else
        {
            restGraceFrame -= 1;
        }
    }

    public void JumpAudio()
    {
        AudioManager.PlayJumpAudio();
    }



    private void FixedUpdate()
    {
        isNearGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        if (isPlayerMove)
        {
            // 场景判断
            if(playScene == PlayScene.normal)
            {
                PlayerMovement();

                if (jumpPressed)
                {
                    handleJumpPressed();
                }
            }
            else if(playScene == PlayScene.river)
            {
                RiverMovement();

                if (jumpPressed)
                {
                    handleRiverJumpPressed();
                }
            }
        }else if (isRush)
        {
            Vector2 tempV2 = transform.position;
            float x = Mathf.MoveTowards(tempV2.x, aimPosition.x, rushSpeed * Time.deltaTime);
            float y = Mathf.MoveTowards(tempV2.y, aimPosition.y, rushSpeed * Time.deltaTime);
            transform.position = new Vector2(x, y);

            if (Vector2.Distance(transform.position, aimPosition) < 0.01f)
            {
                isPlayerMove = true;
                isRush = false;
            }
        }
    }


    //人物移动
    void PlayerMovement()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");

        //if (!isNearGround && xVelocity == 0)
        //   return;

        //根据方向键和加速度acc对角色x轴速度进行控制
        //分为空中加速度和地面加速度两种，前者相对小，后者很大，所以必须保证后者只在地面上时能被应用
        Vector2 tempV = rb.velocity;

        if (isNearGround)
        {
            playerAnimator.SetBool("isJump", false);
            playerAnimator.SetBool("isFall", false);
            playerAnimator.SetFloat("walk", Mathf.Abs(xVelocity));
            rb.velocity = new Vector2(Mathf.MoveTowards(tempV.x, xVelocity * speed, acc * Time.deltaTime), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(tempV.x, xVelocity * speed, accSpace * Time.deltaTime), rb.velocity.y);
            if (rb.velocity.y > 0)
            {
                playerAnimator.SetBool("isJump", true);
            }
            else
            {
                playerAnimator.SetBool("isJump", false);
                playerAnimator.SetBool("isFall", true);
            }
        }
        

        if (xVelocity != 0)
        {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(xVelocity * Mathf.Abs(scale.x), scale.y, 1);
        }
        

        //rb.velocity = new Vector2(xVelocity * speed * Time.deltaTime, rb.velocity.y);
    }

    void RiverMovement()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");
        yVelocity = Input.GetAxisRaw("Vertical");
        Vector2 tempV = rb.velocity;
        rb.velocity = new Vector2(Mathf.MoveTowards(tempV.x, xVelocity * MAXSPDx, accWater * Time.deltaTime), rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, Mathf.MoveTowards(tempV.y, yVelocity * MAXSPDy, accWater * Time.deltaTime));
    }

    //人物跳跃
    public void Jump()
    {
        AudioManager.PlayJumpAudio();
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
    }



    public void BeginRush(Vector3 aimPositon, float rushSpeed)
    {
        Debug.Log("rush begin");
        isPlayerMove = false;
        isRush = true;
        this.aimPosition = aimPositon;
        this.rushSpeed = rushSpeed;
    }


    private void handleJumpPressed()
    {
        if (playerState == 1)
        {
            Debug.Log("jump called in state 1: normal jump");
            bool isJump = false;

            if (isNearGround)
            {
                isJump = true;
            }else if(!isNearGround && restGraceFrame > 0)
            {
                Debug.Log("Grace Jump!");
                isJump = true;
            }

            if (isJump)
            {
                Jump();
            }

        }
        else if (playerState == 2)
        {
            Debug.Log("jump called in state 2: act");
            action();
        }

        jumpPressed = false;
    }

    private void handleRiverJumpPressed()
    {
        Debug.Log("水下互动");
        if (playerState == 2)
        {
            action();
        }
        jumpPressed = false;
    }

    public void changePlayScene(int temp)
    {
        if (temp == 1)
        {
            playScene = PlayScene.normal;
            rb.gravityScale = 1;
        }
        else if (temp == 2)
        {
            playScene = PlayScene.river;
            rb.gravityScale = 0;
        }
        else
        {
            Debug.LogError("changePlayScene Error");
        }
    }

    public void initPlayScene(PlayScene ps)
    {
        if(ps == PlayScene.normal)
        {
            isPlayerMove = true;
            rb.gravityScale = 1;
            rb.bodyType = RigidbodyType2D.Dynamic;
            playScene = PlayScene.normal;

            if(cc2d.direction != 0)
            {
                cc2d.size = new Vector2(cc2d.size.y, cc2d.size.x);
                cc2d.direction = 0;
            }

        }
        else if(ps == PlayScene.river)
        {
            isPlayerMove = true;
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Dynamic;
            playScene = PlayScene.river;

            if (cc2d.direction != (CapsuleDirection2D)1)
            {
                cc2d.size = new Vector2(cc2d.size.y, cc2d.size.x);
                cc2d.direction = (CapsuleDirection2D)1;
            }

        }
        else if(ps == PlayScene.animation)
        {

            isPlayerMove = false;
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Static;
            playScene = PlayScene.animation;
        }
    }

}
