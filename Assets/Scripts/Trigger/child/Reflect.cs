using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    public float moveSpeed = 5f; // 玩家施加的移动力度

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 获取玩家对象
            GameObject player = collision.gameObject;

            // 获取碰撞点和法线向量
            ContactPoint2D contactPoint = collision.contacts[0];
            Vector2 normal = contactPoint.normal;

            // 计算反射向量
            Vector2 inDirection = -player.transform.up;
            Vector2 reflectionDirection = Vector2.Reflect(inDirection, normal);

            // 施加一个移动力度
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.AddForce(reflectionDirection * moveSpeed, ForceMode2D.Impulse);
        }
    }
}
