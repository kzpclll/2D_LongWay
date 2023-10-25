using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTable : MonoBehaviour
{
    [Header("��Ҫ����")]
    public float waitTime;
    public Vector2 moveVector;
    public float moveSpeed;
    [Header("��ʼ�Ƿ񼤻�")]
    public bool isActiveInit;
    [Header("��ɫ����ʱ�Ƿ�ˢ��")]
    public bool isFresh;

    [Header("״̬�������")]
    public bool isActive;
    public bool isWait;
    public int direction;
    public Vector2 aimPosition;
    public PlayerBase player;
    public Vector3 originPosition;
    public bool isAdsorb;

    private Vector3 beginPosition;
    private int state;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        isActive = isActiveInit;
        direction = -1;
        isAdsorb = false;
        beginPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isActive && !isWait)
        {
            if (state == 0)
            {
                StartCoroutine(WaitTime(waitTime));
            }
            else
            {
                Vector2 tempV = transform.position;
                float x = Mathf.MoveTowards(tempV.x, aimPosition.x, moveSpeed * Time.deltaTime);
                float y = Mathf.MoveTowards(tempV.y, aimPosition.y, moveSpeed * Time.deltaTime);
                transform.position = new Vector2(x, y);

                if (isAdsorb)
                {
                    Vector3 offset = transform.position - originPosition;
                    player.transform.position += offset;
                    originPosition = transform.position;
                }

                if (Vector2.Distance(tempV, aimPosition) < 0.01f)
                {
                    state = 0;
                }


            }
        }
    }

    IEnumerator WaitTime(float waitTime)
    {
        isWait = true;
        yield return new WaitForSeconds(waitTime);
        direction = -direction;
        aimPosition = transform.position + direction * (Vector3)moveVector;
        state = direction;
        isWait = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
            isAdsorb = true;
            collision.gameObject.TryGetComponent<PlayerBase>(out player);
            originPosition = transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAdsorb = true;
            collision.gameObject.TryGetComponent<PlayerBase>(out player);
            originPosition = transform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAdsorb = false;
        }
    }

    // ��������ô˷�������movetable
    public void ResetItem()
    {
        if (isFresh)
        {
            transform.position = beginPosition;
            state = 0;
            isActive = isActiveInit;
            direction = -1;
            isAdsorb = false;
        }
    }
    void OnEnable()
    {
        HealthManager healthManager = FindObjectOfType<HealthManager>();
        if (healthManager != null)
        {
            healthManager.RegisterMoveTable(this);
        }
    }

    void OnDisable()
    {
        HealthManager healthManager = FindObjectOfType<HealthManager>();
        if (healthManager != null)
        {
            healthManager.UnregisterMoveTable(this);
        }
    }
}
