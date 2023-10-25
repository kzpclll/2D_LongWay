using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBow : PlayerBase
{
    #region
    // 主要组件和Layer初始化
    public Animator playerAnimator, sailAnimator;
    private PlayerController99 pc99;
    private BoxCollider2D bc2d;
    private CapsuleCollider2D cc2d;
    public SpriteRenderer sr;
    public LayerMask ground;
    public Transform groundCheck;

    //
    private Animator underWaveAni;
    // 移动参数
    [Header("移动参数")]
    public float speed;
    public float acc;
    public float accRow;

    //变量
    public float xVelocity;
    public float yVelocity;
    public Vector2 checkPointPosition;
    private bool isOpenUmbrella;
    private bool isFullSpeed;
    private bool isAutoAcc;
    private bool isLMClicked;
    private bool isRowing;
    private float rowTime;
   
    #endregion
    void Start()
    {
        //获取rigidbody，animation，colider
        pc99 = GetComponent<PlayerController99>();
        rb = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        sailAnimator = transform.Find("sail0").GetComponent<Animator>();
        underWaveAni = GameObject.Find("seaWaveUnder").GetComponent<Animator>();

        xVelocity = 1;
        playerState = 1;
        isPlayerMove = true;
        isFullSpeed = false;
        isAutoAcc = true;
        isLMClicked = false;
        isRowing = true;
        rowTime = 0;
        initPlayScene(playScene);
    }

    void Update()
    {
        OpenUmbrella();
        if (Input.GetMouseButtonDown(0))
        {
            isLMClicked = true;
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerMove)
        {
            // 场景判断
            if (playScene == PlayScene.boat)
            {
                //角色自动移动
                if(speed - rb.velocity.x < 0.01f)
                {
                    isAutoAcc = false;
                    isFullSpeed = true;
                }
                else
                {
                    isFullSpeed = false;
                }
                if (isAutoAcc)
                {
                    Vector2 tempV = rb.velocity;
                    rb.velocity = new Vector2(Mathf.MoveTowards(tempV.x, speed, acc * Time.deltaTime), rb.velocity.y);
                }

                // 角色划船
                if (isLMClicked)
                {
                    if (isFullSpeed)
                    {
                        Debug.Log("left clicked , speed full and no row..");
                    }
                    else
                    {
                        if (!isRowing)
                        {
                            Debug.Log("left clicked, begin row..");
                            playerAnimator.SetTrigger("isRowing");
                            isRowing = true;
                            rowTime = 0;
                        }
                        else
                        {
                            Debug.Log("is Rowing..");
                        }
                    }
                    isLMClicked = false;
                }

                if (isRowing)
                {
                    rowTime += Time.deltaTime;
                    rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, speed, accRow * Time.deltaTime), rb.velocity.y);
                    if (rowTime > 1)
                    {
                        isRowing = false;
                    }
                }
            }
        }
    }


    //人物移动
    void PlayerMovement()
    {

    }


    // modeFlag=true 从待机模式切换为活动模式
    public void ChangeMode(bool modeFlag)
    {
        if (modeFlag)
        {
            rb.velocity = Vector2.zero;
            isPlayerMove = false;
            pc99.enabled = true;
            pc99.oriPosition = transform.position;
            this.enabled = false;
        }
    }

    //开伞 控制状态
    void OpenUmbrella() //Open Umbrella. This method just controll state & animation.
    {
        if (Input.GetKeyDown(KeyCode.Space)) //开伞为鼠标右键(mouse1)此处可修改
        {
            isOpenUmbrella = true;
            playerAnimator.Play("open umbrella");
            playerAnimator.SetBool("isOpenUmbrella", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isOpenUmbrella = false;
            playerAnimator.SetBool("isOpenUmbrella", false);
        }
    }

    //划船动作
    void Rowing()
    {

    }

    //被浪击打时由浪调用此函数
    public void WaveHit()
    {
        Debug.Log("wave effect...");
        rb.velocity += new Vector2(-2, 0);
        sailAnimator.SetTrigger("isHit");
    }

    public void WaveHit(bool flag)
    {
        Debug.Log("special wave blocked...");
        rb.velocity += new Vector2(-2, 0);
        sailAnimator.SetTrigger("isHit");

        DeathEffect.instance.estate = DeathEffect.effectState.close;
        if (!underWaveAni.GetCurrentAnimatorStateInfo(0).IsName("heartwave"))
        {
            underWaveAni.SetTrigger("isHit");
        }

    }

    //每0.02秒判断一次是否触发船帆闪烁
    private void sailCheck() {
        if (!isFullSpeed)
        {
            sailAnimator.SetTrigger("isHit");
        }
    }

    /*
     * 场景切换等通用方法
     */
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
}
