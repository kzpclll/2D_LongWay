using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottlecontroler : MonoBehaviour
{
    public GameObject mySelf;//�蹷�˱���
    public float WaveVelocity;//����ֵ�����蹷��ǰ���ٶȣ������ⲿ���ڣ�������prefab��ȫ�ֵ��ڣ�
    public bool WaveIsHit;
    public bool isOver;
    private Animator anim;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        WaveMoveLeft();
    }

    void WaveMoveLeft()
    {
        if (!WaveIsHit)
        {
            rb.velocity = (Vector2.left * WaveVelocity);
        }

    }

    public void WaveAttack()
    {
        WaveIsHit = true;
        if (anim)
        {
            anim.SetTrigger("attack");
        }
    }

    public void WaveOver()
    {
        Destroy(mySelf);
    }

}
