using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [Header("力度参数")]
    public float upPow;
    public float xPow;

    [Header("冷却时间")]
    public float coolDownTime;
    [Header("检测半径")]
    public float checkRadius;
    [Header("Layer")]
    public LayerMask aimLayer;

    private bool isCoolDown;
    PlayerController player;
    Animator ani;

    private void Awake()
    {
       ani = GetComponent<Animator>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        coolDownTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CoolDown(float time)
    {
        isCoolDown = true;
        yield return new WaitForSeconds(time);
        isCoolDown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCoolDown)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
                {
                    //bool bubbleCheck = Physics2D.OverlapCircle(player.groundCheck.position, checkRadius, gameObject.layer);
                    bool bubbleCheck = true;
                    Debug.Log("isNear" + bubbleCheck);
                    if (bubbleCheck)
                    {
                        CoolDown(coolDownTime);
                        ani.SetTrigger("active");
                        player.rb.velocity = new Vector2(player.rb.velocity.x + xPow, upPow);
                        AudioManager.PlayballoonSound();
                        AudioManager.PlayswishSound();
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCoolDown)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
                {
                    //bool bubbleCheck = Physics2D.OverlapCircle(player.groundCheck.position, checkRadius, gameObject.layer);
                    bool bubbleCheck = true;
                    Debug.Log("isNear" + bubbleCheck);
                    if (bubbleCheck)
                    {
                        CoolDown(coolDownTime);
                        ani.SetTrigger("active");
                        player.rb.velocity = new Vector2(player.rb.velocity.x + xPow, upPow);
                    }
                }
            }
        }
    }
}
