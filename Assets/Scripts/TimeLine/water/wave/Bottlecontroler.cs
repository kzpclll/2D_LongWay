using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottlecontroler : MonoBehaviour
{
    public GameObject mySelf;//疯狗浪本身
    public float WaveVelocity;//此数值决定疯狗浪前进速度，可在外部调节（建议于prefab处全局调节）
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
