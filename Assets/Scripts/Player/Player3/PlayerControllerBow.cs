using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBow : PlayerBase
{
    #region
    // ��Ҫ�����Layer��ʼ��
    public Animator playerAnimator, sailAnimator;
    private PlayerController99 pc99;
    private BoxCollider2D bc2d;
    private CapsuleCollider2D cc2d;
    public SpriteRenderer sr;
    public LayerMask ground;
    public Transform groundCheck;

    //
    private Animator underWaveAni;
    // �ƶ�����
    [Header("�ƶ�����")]
    public float speed;
    public float acc;
    public float accRow;

    //����
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
        //��ȡrigidbody��animation��colider
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
            // �����ж�
            if (playScene == PlayScene.boat)
            {
                //��ɫ�Զ��ƶ�
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

                // ��ɫ����
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


    //�����ƶ�
    void PlayerMovement()
    {

    }


    // modeFlag=true �Ӵ���ģʽ�л�Ϊ�ģʽ
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

    //��ɡ ����״̬
    void OpenUmbrella() //Open Umbrella. This method just controll state & animation.
    {
        if (Input.GetKeyDown(KeyCode.Space)) //��ɡΪ����Ҽ�(mouse1)�˴����޸�
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

    //��������
    void Rowing()
    {

    }

    //���˻���ʱ���˵��ô˺���
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

    //ÿ0.02���ж�һ���Ƿ񴥷�������˸
    private void sailCheck() {
        if (!isFullSpeed)
        {
            sailAnimator.SetTrigger("isHit");
        }
    }

    /*
     * �����л���ͨ�÷���
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
