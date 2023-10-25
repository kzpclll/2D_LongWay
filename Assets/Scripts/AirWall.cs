using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWall : MonoBehaviour
{
    public GameObject CameraFoucsPoint;
    public Vector3 offset;

    void Awake()
    {
        offset = transform.position - CameraFoucsPoint.transform.position;
    }

    void LateUpdate()
    {
        transform.position = CameraFoucsPoint.transform.position + offset;
    }


}
