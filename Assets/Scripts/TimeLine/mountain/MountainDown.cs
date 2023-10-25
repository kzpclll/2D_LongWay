using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainDown : MonoBehaviour
{
    Camera02 camera02;
    // Start is called before the first frame update
    void Start()
    {
        camera02 = Camera.main.GetComponent<Camera02>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimelineEnd()
    {
        if (camera02)
        {
            Debug.Log("TimelineEnd");
            camera02.InitCamerainfo(GameObject.Find("Player/Player_3").GetComponent<PlayerBase>()); ;
        }

    }
}
