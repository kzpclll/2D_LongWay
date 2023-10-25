using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 该摄像头对目标在X轴上保持固定偏移值
public class Camera01 : CameraBase
{
    private Vector2 offset;
    private Vector3 tempV; 
    public bool isFoucs;

    void Start()
    {
        isFoucs = true;
        tempV = CameraFoucsPoint.transform.position;

        transform.position = new Vector3( tempV.x,tempV.y, -5);
        offset = transform.position - tempV;
    }

    void LateUpdate()
    {
        if (isFoucs)
        {
            Vector2 tempV = transform.position;
            transform.position = new Vector3(CameraFoucsPoint.transform.position.x + offset.x, tempV.y, -1);
        }
    }
}
