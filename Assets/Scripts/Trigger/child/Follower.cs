using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    
    public float moveSpd;
    public float jumpPow;

    private float tendDistance;
    private Rigidbody2D rb;
    private bool isActive;
    [Header("Debug")]
    private int listIndex;
    private Transform playerPosition;
    public float dis;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.Find("Player/kid_movecenter").transform;
        tendDistance = FollowerController.instance.tendDistance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            Movement();
        }
    }

    private void Movement()
    {
        
        float temp = (playerPosition.position.x- transform.position.x) - (listIndex+1)*tendDistance;
        dis = temp;
        temp = moveSpd + 0.8f * temp;
        rb.velocity = new Vector2(temp, rb.velocity.y);
    }

    // PlayerController调用，跳跃
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPow);
    }

    // PlayerKid调用，开始移动
    public void WaitActive()
    {
        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        int temp;
        isActive = false;
        temp = FollowerController.instance.AddFollower(this);
        if (temp != -1)
        {
            listIndex = temp;
        }
        yield return new WaitForSeconds(FollowerController.instance.intervalTime * (FollowerController.instance.followerList.Count));
        isActive = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<BoxCollider2D>().isTrigger = false;
        
    }
}
