using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLine : MonoBehaviour
{
    public float speed = 5f; // 旋转速度
    private float timer = 0f; // 计时器
    public float minTime = 1f;
    public float maxTime = 1.5f;
    private bool isRight;
    private Vector3 startPosition; // 初始位置

    void Start()
    {
        startPosition = transform.position; // 记录初始位置
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

