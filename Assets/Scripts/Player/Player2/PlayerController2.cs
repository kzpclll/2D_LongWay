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

    // �ƶ�����
    [Header("����ƶ��ٶ�")]
    public float speed;
    [Header("������ٶ�")]
    public float acc;
    [Header("���м��ٶ�")]
    public float accSpace;

    // ��Ծ����
    [Header("����֡")]
    public float graceFrame = 6;
    private float restGraceFrame;

    [Header("�������Y�������ٶȣ���ɡʱ��ƽ�����ٶ�)")]
    public float maxFallSpeed;
    [Header("��ɡ���ٶȣ���ɡ�ƽ���������ٶȵļ��ٶȣ�")]
    public float approximateAcc;

    // ��Ծ���������
    [HideInInspector]
    private bool isNearGround;

    // ״̬����

    // playerState����Jump�����¼� 1Ĭ��
    // isPlayerMove �Ƿ���ܲ���
    [Header("״̬����������һ�㲻�ģ�")]
    // public int playerState;

    //����
    public float xVelocity;
    public float yVelocity;
    public Vector2 checkPointPosition;

    void Start()
    {
        //��ȡrigidbody��animation��colider
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
        //����Jump���� 
        jumpPressed = Input.GetButton("Jump");
        


        //����ʱ�� ������վ�ء�����
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
            // �����ж�
            if (playScene == PlayScene.normal)
            {
                if(handleJumpPressed() != 0)
                {
                    PlayerMovement();
                }
            }
        }
    }


    //�����ƶ�
    void PlayerMovement()
    {
        playerAnimator.SetFloat("vY", rb.velocity.y);
        xVelocity = Input.GetAxisRaw("Horizontal");

        //���ݷ�����ͼ��ٶ�acc�Խ�ɫx���ٶȽ��п���
        //��Ϊ���м��ٶȺ͵�����ٶ����֣�ǰ�����С�����ߺܴ����Ա��뱣֤����ֻ�ڵ�����ʱ�ܱ�Ӧ��

        Vector2 tempV = rb.velocity;

        if (isNearGround)
        {
            //  ����ʱ���ƶ�
            playerAnimator.SetFloat("walk", Mathf.Abs(xVelocity));
            playerAnimator.SetBool("isfly", false);
            rb.velocity = new Vector2(Mathf.MoveTowards(tempV.x, xVelocity * speed, acc * Time.deltaTime), rb.velocity.y);
        }
        else
        {   //  ���е��ƶ�
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
