using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaby : PlayerBase
{
    [Header("移动参数")]
    public float moveSpeed;

    private float xVelocity;
    private bool isSpacePressed;
    private bool isCry;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isCry = false;
    }

    // Update is called once per frame
    void Update()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            isSpacePressed = true;
        }
    }

    private void FixedUpdate()
    {
        if (isSpacePressed)
        {
            Cry();
            isSpacePressed = false;
        }

        if (!isCry)
        {
            Movement();
        }
    }

    private void Movement()
    {
        animator.SetFloat("xVelocity", Mathf.Abs( xVelocity));
        rb.velocity = new Vector2(xVelocity * moveSpeed, rb.velocity.y);
        if (xVelocity != 0)
        {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(xVelocity * Mathf.Abs(scale.x), scale.y, 1);
        }
    }

    private void Cry()
    {
        if (!isCry)
        {
            isCry = true;
            animator.SetTrigger("isCry");
            AudioManagerC.PlayCryingAudio();
        }
    }
    
    /*
     * 被animator调用
     */
    private void callLamps()
    {
        LampController.sendMsg2Lamps();
    }

    private void endCry()
    {
        isCry = false;
        Movement();
    }

}
