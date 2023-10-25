using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControl : MonoBehaviour
{
    public GameObject mySelf;//�蹷�˱���
    [Header("�����ٶ�")]
    public float WaveVelocity;//����ֵ�����蹷��ǰ���ٶȣ������ⲿ���ڣ�������prefab��ȫ�ֵ��ڣ�
    [HideInInspector]
    public bool WaveIsHit;
    [HideInInspector]
    public bool isOver;
    private Animator anim;
    [HideInInspector]
    public Rigidbody2D rb;

    [Header("���Ʒ���Ч��(��ҩƿ��Ч)")]
    public float bounceSpeed = 3f;
    public float bounceAcc = 1;

    [Header("�������ٶ����������Χ(��ҩƿ��Ч)")]
    public float randomMin = -0.5f;
    public float randomMax = 0.5f;

    [Header("�³����ٶ�(��ҩƿ��Ч)")]
    public float sinkAcc = 3;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }



    void Update()
    {
        if (!WaveIsHit)
        {
            WaveMoveLeft();
        }
        else
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0, bounceAcc*Time.deltaTime), 0);
            rb.velocity += new Vector2(0, -1 * sinkAcc * Time.deltaTime);
        }
    }

    // �����װЧ��
    void WaveMoveLeft()
    {
        rb.velocity = (Vector2.left * WaveVelocity);
    }

    // ��ײ�����Ժ���������ô˺���
    public void WaveAttack()
    {
        if (!WaveIsHit)
        {
            WaveIsHit = true;
            // animation = true ���� = false ҩƿ
            if (anim)
            {
                anim.SetTrigger("attack");
            }
            else
            {
                rb.velocity = new Vector2(bounceSpeed + Random.Range(randomMin, randomMax), 0);
            }

            StartCoroutine("DelayDestory");
        }
    }

    public void WaveOver()
    {
        Destroy(gameObject);
    }

    IEnumerator DelayDestory()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }

}
