using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    // ����ģʽ
    public static DeathEffect instance; 

    Transform up, down;
    private Vector2 oriPositionUp;
    private Vector2 oriPositionDown;
    [Header("��ǰ״̬")]
    public effectState estate;
    
    private Rigidbody2D rbUp;
    private Rigidbody2D rbDown;
    private float clickTimer;

    [Header("�Զ��ƶ�����")]
    public float maxSpeed;
    public float effectAcc;
    [Header("����������")]
    public float clickCD;
    public float clickAcc;
    public float waitSeconds;
    public int clickNum;

    private int clickCount;
    private bool isClickCD;
    [HideInInspector]
    public bool isCanbeClick;
    public enum effectState{
        open,
        close,
        wait
    }

    // ��ʼ������Effect.instance
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        up = transform.Find("up");
        down = transform.Find("down");
        isCanbeClick = true;
        initPositionInfo();

        estate = effectState.wait;
        clickTimer = 0;
        clickCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isCanbeClick)
            {
                click();
            }
        }
    }

    public void FixedUpdate()
    {
        // ����״ֵ̬���ƺ�Ļ
        if (estate == effectState.close)
        {
            Vector2 myValue = new Vector2(0, Mathf.MoveTowards(rbUp.velocity.y, -maxSpeed, effectAcc));
            rbUp.velocity = myValue;
            // Debug.Log("now value:" + myValue);
            rbDown.velocity = -1 * rbUp.velocity;
        }
        else if (estate == effectState.open)
        {
            Vector2 myValue = new Vector2(0, Mathf.MoveTowards(rbUp.velocity.y, maxSpeed, effectAcc));
            rbUp.velocity = myValue;
            rbDown.velocity = -1 * rbUp.velocity;

            if (rbUp.position.y - oriPositionUp.y > 0.1f)
            {
                wait();
            }
        }
        else
        {
            //estate==wait do nothing
        }

        //Timer
        if (isClickCD)
        {
            clickTimer += Time.fixedDeltaTime;
            if (clickTimer > clickCD)
            {
                clickTimer = 0;
                isClickCD = false;
            }
        }
    }



    public void initPositionInfo()
    {
        oriPositionUp = up.position;
        oriPositionDown = down.position;

        rbUp = up.GetComponent<Rigidbody2D>();
        rbDown = down.GetComponent<Rigidbody2D>();
    }

    public void closeEye()
    {
        estate = effectState.close;
    }

    public void openEye()
    {
        estate = effectState.open;
    }

    public void wait()
    {
        estate = effectState.wait;
        rbUp.velocity = Vector2.zero;
        rbDown.velocity = Vector2.zero;
    }

    // ����¼�
    public void click()
    {
        if(estate == effectState.close && !isClickCD)
        {
            clickCount += 1;
            isClickCD = true;

            rbUp.velocity += new Vector2(0, clickAcc);
            rbDown.velocity += new Vector2(0, -clickAcc);

            // ��clickNum�κ�������
            if (clickCount > clickNum)
            {
                this.estate = effectState.open;
                clickCount = 0;
            }
        }
    }

    public void SpecialWaveEvent()
    {
        instance.estate = effectState.close;
        StartCoroutine("LockClickForSeconds");
    }

    IEnumerator LockClickForSeconds()
    {
        instance.isCanbeClick = false;
        yield return new WaitForSeconds(waitSeconds);
        instance.isCanbeClick = true;
    }
}
