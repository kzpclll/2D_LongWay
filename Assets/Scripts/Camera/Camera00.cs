using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ͷ��ȫ����Ŀ��
public class Camera00: CameraBase
{
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - CameraFoucsPoint.transform.position;
    }

    void LateUpdate()
    {
        transform.position = CameraFoucsPoint.transform.position + offset;
    }
}
