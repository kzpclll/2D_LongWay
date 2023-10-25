using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBKG : MonoBehaviour
{
    // 第一个背景sprite 的位置、相机相对偏移、长度
    public GameObject bkg;
    private Vector3 bkg_pos;
    private float iniCameraOffset;
    private float sizex;

    //相机中心、相机边界
    public GameObject focusPoint;
    private float cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        //
        bkg_pos = bkg.transform.position;
        focusPoint = GameObject.Find("MainCamera/focusPoint").gameObject;
        iniCameraOffset = bkg_pos.x - focusPoint.transform.position.x;
        cameraOffset = GameObject.Find("MainCamera/leftAirWall").gameObject.GetComponent<AirWall>().offset.x;

        Debug.Log(iniCameraOffset);
        Debug.Log(cameraOffset);
        sizex = bkg.GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform tran in transform)
        {
            Vector3 pos = tran.position;
            if (pos.x + sizex < focusPoint.transform.position.x + cameraOffset)
            {
                pos.x += 4 * sizex;
            }
            tran.position = pos;
        }

    }
}
