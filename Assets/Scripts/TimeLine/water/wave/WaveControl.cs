using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControl : MonoBehaviour
{
    public GameObject mySelf;//疯狗浪本身
    [Header("道具速度")]
    public float WaveVelocity;//此数值决定疯狗浪前进速度，可在外部调节（建议于prefab处全局调节）
    [HideInInspector]
    public bool WaveIsHit;
    [HideInInspector]
    public bool isOver;
    private Animator anim;
    [HideInInspector]
    public Rigidbody2D rb;

    [Header("控制反弹效果(仅药瓶有效)")]
    public float bounceSpeed = 3f;
    public float bounceAcc = 1;

    [Header("反弹初速度随机常数范围(仅药瓶有效)")]
    public float randomMin = -0.5f;
    public float randomMax = 0.5f;

    [Header("下沉加速度(仅药瓶有效)")]
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

    // 添加碰装效果
    void WaveMoveLeft()
    {
        rb.velocity = (Vector2.left * WaveVelocity);
    }

    // 碰撞到船以后子物体调用此函数
    public void WaveAttack()
    {
        if (!WaveIsHit)
        {
            WaveIsHit = true;
            // animation = true 海浪 = false 药瓶
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
