using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceController : MonoBehaviour
{
    public Transform target; // ��תĿ������
    public float speed = 5f; // ��ת�ٶ�
    public bool isRotationEnabled = true; 
    private Vector3 axis; // ��ת������
    private float timer = 0f; // ��ʱ��
    public float minTime = 1f;
    public float maxTime = 1.5f;

    void Start()
    {
        axis = new Vector3(0, 0, -1); // ���ó�ʼ��ת��������z�Ḻ����
    }

    void Update()
    {
        if (isRotationEnabled)
        {
            transform.RotateAround(target.position, axis, speed * Time.deltaTime); // Χ��Ŀ����ת
            axis = Quaternion.Euler(0, 0, speed * Time.deltaTime) * axis; // ������ת������
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
