using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : PlayerBase
{
    public Animator playerAnimator;
    private BoxCollider2D bc2d;
    private CapsuleCollider2D cc2d;
    public SpriteRenderer sr;
    public LayerMask ground;
    public Transform groundCheck;
    public BuoyancyEffector2D buoyancyEffector2D;

    // 移动参数
    [Header("最大移动速度")]
    public float speed;
    [Header("地面加速度")]
    public float acc;
    [Header("空中加速度")]
    public float accSpace;

    // 跳跃参数
    [Header("土狼帧")]
    public float graceFrame = 6;
    private float restGraceFrame;

    [Header("空中最大Y轴下落速度（开伞时会逼近这个速度)")]
    public float maxFallSpeed;
    [Header("开伞加速度（开伞逼近最大下落速度的加速度）")]
    public float approximateAcc;

    // 跳跃、互动检测
    [HideInInspector]
    private bool isNearGround;

    // 状态控制

    // playerState控制Jump按键事件 1默认
    // isPlayerMove 是否接受操作
    [Header("状态参数（以下一般不改）")]
    // public int playerState;

    //变量
    public float xVelocity;
    public float yVelocity;
    public Vector2 checkPointPosition;

    void Start()
    {
        //获取rigidbody，animation，colider
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();
        cc2d = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        buoyancyEffector2D = FindObjectOfType<BuoyancyEffector2D>();


        restGraceFrame = 0;

        playerState = 1;

        initPlayScene(playScene);

    }
    public void PlayopenUmbrellaSound()
    {
        AudioManager.PlayopenUmbrellaSound();
    }
    void Update()
    {
        //接收Jump按键 
        jumpPressed = Input.GetButton("Jump");
        


        //土狼时间 起跳、站地、落下
        if (rb.velocity.y > 0.01f && isNearGround)
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

    private void FixedUpdate()
    {
        isNearGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        if (isPlayerMove)
        {
            // 场景判断
            if (playScene == PlayScene.normal)
            {
                if(handleJumpPressed() != 0)
                {
                    PlayerMovement();
                }
            }
        }
    }


    //人物移动
    void PlayerMovement()
    {
        playerAnimator.SetFloat("vY", rb.velocity.y);
        xVelocity = Input.GetAxisRaw("Horizontal");

        //根据方向键和加速度acc对角色x轴速度进行控制
        //分为空中加速度和地面加速度两种，前者相对小，后者很大，所以必须保证后者只在地面上时能被应用

        Vector2 tempV = rb.velocity;

        if (isNearGround)
        {
            //  地面时的移动
            playerAnimator.SetFloat("walk", Mathf.Abs(xVelocity));
            playerAnimator.SetBool("isfly", false);
            rb.velocity = new Vector2(Mathf.MoveTowards(tempV.x, xVelocity * speed, acc * Time.deltaTime), rb.velocity.y);
        }
        else
        {   //  空中的移动
            rb.velocity = new Vector2(Mathf.MoveTowards(tempV.x, xVelocity * speed, accSpace * Time.deltaTime), rb.velocity.y);
            playerAnimator.SetBool("isfly", true);
        }


        if (xVelocity != 0)
        {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(xVelocity * Mathf.Abs(scale.x), scale.y, 1);
        }

    }



    private int handleJumpPressed()
    {
        int res = 1;
        if (playerState == 1)
        {
            if (jumpPressed)
            {
                playerAnimator.SetBool("sanUP", true);
                if (isNearGround)
                {
                    res = 0;
                }
                if (buoyancyEffector2D)
                {
                    if (!buoyancyEffector2D.enabled)
                    {
                        buoyancyEffector2D.enabled = true;
                    }
                }
                if (rb.velocity.y < -maxFallSpeed)
                {
                    // Debug.Log("rb.v:" + rb.velocity.y);
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.MoveTowards(rb.velocity.y, maxFallSpeed, approximateAcc * Time.deltaTime));
                }
            }
            else
            {
                playerAnimator.SetBool("sanUP", false);

                if (buoyancyEffector2D)
                {
                    if (buoyancyEffector2D.enabled)
                    {
                        buoyancyEffector2D.enabled = false;
                    }
                }
            }
            
        }
        else if (playerState == 2)
        {
            //Debug.Log("jump called in state 2: act");
            action();
        }

        jumpPressed = false;
        return res;
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
        if (ps == PlayScene.normal)
        {
            isPlayerMove = true;
            rb.gravityScale = 1;
            rb.bodyType = RigidbodyType2D.Dynamic;
            playScene = PlayScene.normal;

            if (cc2d.direction != 0)
            {
                cc2d.size = new Vector2(cc2d.size.y, cc2d.size.x);
                cc2d.direction = 0;
            }

        }
        else if (ps == PlayScene.river)
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
        else if (ps == PlayScene.animation)
        {

            isPlayerMove = false;
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Static;
            playScene = PlayScene.animation;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WindEffector"))
        {
            this.buoyancyEffector2D = collision.gameObject.GetComponent<BuoyancyEffector2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WindEffector"))
        {
            this.buoyancyEffector2D = null;
        }
    }
}
