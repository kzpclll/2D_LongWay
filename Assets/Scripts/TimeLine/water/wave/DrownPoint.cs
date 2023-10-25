using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrownPoint : MonoBehaviour
{
    public float Velocity;
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = (Vector2.left * Velocity);
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            player.GetComponent<PlayerController99>().BlockWave(false,false);
        }
    }
}
