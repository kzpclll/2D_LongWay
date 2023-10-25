using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public float moveSpd;
    public float jumpPow;
    private Animator animator;
    private Rigidbody2D rb;
    public LeaderMom wife;
    public bool isActive = false;
    private Vector3 leaderPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        leaderPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isActive == true)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            Movement();
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
    private void Movement()
    {
        rb.velocity = new Vector2(moveSpd, rb.velocity.y);
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPow);
        animator.Play("jump");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("JumpTrigger"))
        {
            Jump();
            wife.Jump2();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.Play("glides");
            wife.Move2();
        }
    }
    public void Reset()
    {
        isActive = false;
        animator.Play("father");
        transform.position = leaderPosition;
        wife.Idle();
    }

}
