using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKGController : MonoBehaviour
{
    public GameObject mySelf;
    public float MoveVelocity;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        MoveLeft();
    }

    void MoveLeft()
    { 
        rb.velocity = (Vector2.left * MoveVelocity);
    }

}
