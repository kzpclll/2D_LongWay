using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 该摄像头与目标在X轴上的坐标同时向正方向前进，但无法后退
public class Camera02 : CameraBase
{
    private float diff;
    private Vector3 tempV;
    public bool isFocus;
    public bool isFollow;
    public float forwardOnlySpeed;
    public float autoForwardSpeed;
    
    public enum CameraMode
    {
        forwardOnly,
        autoForward,
        keepOffset,
        free
    }
    public CameraMode cameraMode;
    public 
    void Start()
    {
        isFocus = true;
        isFollow = false;
        Debug.Log("Camera02 start");
        player = FindObjectOfType<PlayerBase>();
        InitCamerainfo(player);
        // 相机中心与CameraFocusPoint对齐
        // Invoke("AlignPlayer", 0.5f);
        // tempV = CameraFoucsPoint.transform.position;
        // transform.position = new Vector3(tempV.x, tempV.y + 2.5f, -5);
    }

    void LateUpdate()
    {
        if (isFocus)
        {
            if(cameraMode == CameraMode.forwardOnly)
            {
                diff = CameraFoucsPoint.transform.position.x - transform.position.x;
                if (diff > 0.01f)
                {
                    float x = Mathf.MoveTowards(transform.position.x, CameraFoucsPoint.transform.position.x, forwardOnlySpeed);
                    float y = Mathf.MoveTowards(transform.position.y, CameraFoucsPoint.transform.position.y, forwardOnlySpeed);
                    transform.position = new Vector3(x, transform.position.y, -1);
                }
            }else if(cameraMode == CameraMode.autoForward)
            {
                transform.position += new Vector3(autoForwardSpeed * Time.deltaTime, 0, 0);
                diff = CameraFoucsPoint.transform.position.x - transform.position.x;
                if (diff > 0.01f)
                {
                    float x = Mathf.MoveTowards(transform.position.x, CameraFoucsPoint.transform.position.x, forwardOnlySpeed);
                    transform.position = new Vector3(x, transform.position.y, -1);
                }
            }else if(cameraMode == CameraMode.keepOffset)
            {
                float aimPositon = CameraFoucsPoint.transform.position.x + cameraOffset.x;
                diff = CameraFoucsPoint.transform.position.x + cameraOffset.x - transform.position.x;
                if (diff > 0.01f)
                {
                    float x = Mathf.MoveTowards(transform.position.x, aimPositon, forwardOnlySpeed);
                    transform.position = new Vector3(x, transform.position.y, -1);
                }
            }else if(cameraMode == CameraMode.free)
            {
                Vector2 aimPoisition = (Vector2)CameraFoucsPoint.transform.position + cameraOffset;
                transform.position = new Vector3(aimPoisition.x,aimPoisition.y,-1);
                
            }
        }
    }
}
