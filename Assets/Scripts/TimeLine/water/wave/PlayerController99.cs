using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController99 : MonoBehaviour
{
    public bool isRowing;
    public bool isOpenUmbrella;
    public bool isHit;
    public bool isBlockWaveAndHitBack;
    public bool isDead;
    //���ϱ��������жϣ��������޸�Ϊprivate

    public float rowingTimer;
    public float forwardSpeed;
    public float backwardSpeed;
    public float sinkTimer;
    public float blockWaveTimer;

    public float presentLocationX;
    public float targetLocationX;
    public Vector2 targetPosition;
    public float smoothTime = 0.5f;//For limit range movement each click operation

    Vector2 velocity = Vector2.zero;
    public Vector3 dir;
    public Vector3 dirBack;

    public Animator anim;
    public Animator anim_moonSail;
    public Rigidbody2D rb;

    public GameObject moonSail;//����������moonsail
    public GameObject Player;

    public bool isFreeze;
    public float TimeOfFreeze;
    public float FreezeTimer;//��ֹ������ϵ����λ

    //�༭�����ӿ�
    public bool isDragPlayerDown;//ǿ�ƺ����³�

    public bool isLeavingCentral;
    public bool reachedCentral;
    public Vector2 currentVelocity;
    public LayerMask Blocker;
    public Transform touchPoint;// ����ǽӴ����м��stopper

    [HideInInspector]
    public Vector2 oriPosition;

    public AtAimPosition aap;
    private bool isLeftClicked;
    private Animator underWaveAni;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim_moonSail = moonSail.GetComponent<Animator>();
        underWaveAni = GameObject.Find("seaWaveUnder").GetComponent<Animator>();
    }

    void Update()
    {
        Rowing();
        OpenUmbrella();
        dragPlayerDrown();

        // Debug.Log("reachedCentral = " + reachedCentral);
        // Debug.Log("isLeavingCentral = " + isLeavingCentral);
        // reachedCentral = true �� isLeavingCentral = falseʱ�����»�
        if (Vector2.Distance(transform.position, touchPoint.position)<0.1f)
        {
            reachedCentral = true;
        }
        else
        {
            reachedCentral = false;
        }
        // reachedCentral = Physics2D.OverlapCircle(touchPoint.position, 0.1f, Blocker);
        if (reachedCentral &&/* currentVelocity != Vector2.zero &&*/ !isLeavingCentral)
        {
            rb.velocity = new Vector2(0, 0);
            isDragPlayerDown = false;
            isRowing = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isLeftClicked = true;
        }
    }
    public void AccelerateSound()
    {
        AudioManager.PlayAccelerateSound();
    }
    public void UmbrellaSound()
    {
        AudioManager.PlayopenUmbrellaSound();
    }

    private void FixedUpdate()
    {
        Sinking();
        if(isBlockWaveAndHitBack)
        {
            BlockWave(false,false);
        }
        presentLocationX = this.transform.position.x;

        currentVelocity = rb.velocity;
    }

    

    // ÿһ֡�³� FixUpdate ����boolֵ���ý�ɫ�ٶ�
    void Sinking()
    {
        // isDragPlayerDown
        if(isDragPlayerDown && !isDead && !isRowing)
        {
            rb.AddForce((Vector2)(-dirBack * 0.8f));
            rb.AddForce((Vector2)(Vector2.down * 0.1f));
        }

        /*if (isDead)
        {
            rb.velocity = (Vector2)(Vector2.down * 0.5f);
        }*/
    }

    // ������� Update
    void Rowing() //Click to row Player's liitle boat forward(actually rightward)
    {
        rowingTimer += Time.deltaTime;
        // ������� û�п�ɡ��û��Freeze ��ʼ��λ�� ��ִ�л���
        if(isLeftClicked && !isOpenUmbrella && !isRowing &&!isFreeze) //����Ϊ������(mouse0)�˴����޸�
        {
            rowingTimer = 0f;
            isRowing = true;

            targetLocationX = presentLocationX + 2f;
            targetPosition = new Vector3(targetLocationX, this.transform.position.y + 0.5f);

            rb.velocity = new Vector2(0, 0);
            isLeavingCentral = false;
        }

        // ��������
        if (isRowing)
        {
            anim.SetTrigger("isRowing");
            Player.transform.position = Vector2.SmoothDamp(current: Player.transform.position, targetPosition, ref velocity, smoothTime, forwardSpeed);
        }

        // isRowing��true��Ϊfalse����Сʱ��ΪmagicNumber=1s 
        if (rowingTimer > 1)
        {
            isRowing = false;
            rowingTimer = 0;
        }
        isLeftClicked = false;
    }

    
    //��ɡ��� Update ���ƿ�ɡ��������isOpenUmbrella
    void OpenUmbrella() //Open Umbrella. This method just controll state & animation.
    { 
        if(Input.GetKeyDown(KeyCode.Space)) //��ɡΪ����Ҽ�(mouse1)�˴����޸�
        {
            isOpenUmbrella = true;
            anim.Play("open umbrella");
            anim.SetBool("isOpenUmbrella", true);
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            isOpenUmbrella=false;
            anim.SetBool("isOpenUmbrella", false);
        }       
    }

    // ����񵲳ɹ� ����Bool FixUpdate
    public void BlockWave(bool isEffectWave, bool isHeartWave)//Open Player's little umbrella to block surge. Better not copy this hopeless action in reality.
    {
        //Debug.Log("block wave");
        /*anim_moonSail.SetTrigger("isHit");*/
        blockWaveTimer += Time.deltaTime;
        isBlockWaveAndHitBack = true;

        /*rb.velocity = (Vector2)(Vector2.left * backwardSpeed);*/
        if (blockWaveTimer > 1f)
        {
            isBlockWaveAndHitBack = false;
            blockWaveTimer = 0;
        }

        if (isEffectWave)
        {
            DeathEffect.instance.SpecialWaveEvent();

        }

        if (isHeartWave)
        {
            if (!underWaveAni.GetCurrentAnimatorStateInfo(0).IsName("heartwave"))
            {
                underWaveAni.SetTrigger("isHit");
            }
        }
    }

    public void getHit() //Actually For Kill Player
    {
        if (!isDead)
        {
            aap.ResetTimeline();
            anim.SetBool("isDead", true);
            anim_moonSail.SetTrigger("isHit");
            isDead = true;
            StartCoroutine("waitRespwn");
        }
    }

    
    // �³���� Update ������һЩbool
    public void dragPlayerDrown()//Force Player Drown, which make it move leftward & downward.
    {
        // ����D�� ���� isBlockWaveAndHitBack
        if (Input.GetMouseButtonDown(1) || isBlockWaveAndHitBack)//Just For Testing
        {
            //Debug.Log("isDragingPlayerDown");
            isFreeze = true;
            isLeavingCentral = true;
            anim_moonSail.SetTrigger("isHit");
            isDragPlayerDown = true;
        }
         
        if(isFreeze)
        {
            FreezeTimer += Time.deltaTime;
        }

        if(FreezeTimer > TimeOfFreeze)
        {
            isFreeze = false;
            FreezeTimer = 0f;
        }
    }

    public void hitDeathZone()
    {
        isDead = true;
    }

    public void playerDead()
    {
        isDead = true;
        StartCoroutine("waitRespwn");

    }

    public void playerRespwn()
    {
        anim.SetBool("isDead", false);
        DeathEffect.instance.estate = DeathEffect.effectState.open;
        DeathEffect.instance.isCanbeClick = true;
        //anim_moonSail.SetTrigger("isHit");
        isDead = false;
    }


    IEnumerator waitRespwn()
    {
        yield return new WaitForSeconds(2);
        DeathEffect.instance.estate = DeathEffect.effectState.close;
        DeathEffect.instance.isCanbeClick = false;
        yield return new WaitForSeconds(4);
        transform.position = oriPosition;
        isLeftClicked = true;
        StartCoroutine("playerRespwn");
    }
}
