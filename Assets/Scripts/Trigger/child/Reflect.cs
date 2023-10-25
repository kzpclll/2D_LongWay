using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    public float moveSpeed = 5f; // ���ʩ�ӵ��ƶ�����

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��ȡ��Ҷ���
            GameObject player = collision.gameObject;

            // ��ȡ��ײ��ͷ�������
            ContactPoint2D contactPoint = collision.contacts[0];
            Vector2 normal = contactPoint.normal;

            // ���㷴������
            Vector2 inDirection = -player.transform.up;
            Vector2 reflectionDirection = Vector2.Reflect(inDirection, normal);

            // ʩ��һ���ƶ�����
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.AddForce(reflectionDirection * moveSpeed, ForceMode2D.Impulse);
        }
    }
}
