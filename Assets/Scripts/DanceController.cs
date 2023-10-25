using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceController : MonoBehaviour
{
    public Transform target; // 旋转目标物体
    public float speed = 5f; // 旋转速度
    public bool isRotationEnabled = true; 
    private Vector3 axis; // 旋转轴向量
    private float timer = 0f; // 计时器
    public float minTime = 1f;
    public float maxTime = 1.5f;

    void Start()
    {
        axis = new Vector3(0, 0, -1); // 设置初始旋转轴向量（z轴负方向）
    }

    void Update()
    {
        if (isRotationEnabled)
        {
            transform.RotateAround(target.position, axis, speed * Time.deltaTime); // 围绕目标旋转
            axis = Quaternion.Euler(0, 0, speed * Time.deltaTime) * axis; // 更新旋转轴向量
        }

        timer += Time.deltaTime;
        if (timer >= minTime)
        {
            isRotationEnabled = false;   
        }
        if(timer >= maxTime)
        {
            isRotationEnabled = true;
            timer = 0f;
        }
    }
}
