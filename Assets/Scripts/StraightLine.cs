using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLine : MonoBehaviour
{
    public float speed = 5f; // ��ת�ٶ�
    private float timer = 0f; // ��ʱ��
    public float minTime = 1f;
    public float maxTime = 1.5f;
    private bool isRight;
    private Vector3 startPosition; // ��ʼλ��

    void Start()
    {
        startPosition = transform.position; // ��¼��ʼλ��
    }

    void Update()
    {
        if (isRight)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

        timer += Time.deltaTime;
        if (timer >= minTime)
        {
            isRight = false;
        }
        if (timer >= maxTime)
        {
            isRight = true;
            timer = 0f;
        }
    }
}

