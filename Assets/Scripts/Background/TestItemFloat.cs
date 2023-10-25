using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ʒ����Ч��
/// </summary>
public class TestItemFloat : MonoBehaviour
{
    //Ư������
    public float speed = 1f;
    //Ư����ΧX,Y,Z
    public Vector3 offsetXYZ = new Vector3(1, 1, 1);
    //ԭ��
    private Vector3 originalPos;

    void Start()
    {
        if (speed <= 0)
        {
            speed = 1f;
        }
        //��ȡԭ��
        originalPos = transform.localPosition;
    }

    void Update()
    {
        //�������Ǻ�����ʱ��Ϊ�仯��ģ�⸡��Ч��(������ϲ�����ƫ�Ƶ��ǳ��ϵ�ֵ����Ҫ��[-1,1]����Χ�ڱ仯)
        transform.localPosition = originalPos + new Vector3(offsetXYZ.x * Mathf.Sin(Time.time * speed), offsetXYZ.y * Mathf.Cos(Time.time * speed) * Mathf.Sin(Time.time * speed), offsetXYZ.z * Mathf.Cos(Time.time * speed));
    }
}