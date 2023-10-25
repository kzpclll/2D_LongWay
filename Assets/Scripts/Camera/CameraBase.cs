using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
    public GameObject CameraFoucsPoint;
    public Vector2 cameraOffset;
    public float cameraSize;
    public Vector2 oriCameraPosition;

    public PlayerBase player;
    public PlayerHealth ph;

    // Start is called before the first frame update
    void Start()
    {
        oriCameraPosition = transform.position;
        cameraOffset = transform.position - CameraFoucsPoint.transform.position;
        cameraSize = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CameraFresh()
    {
        Debug.Log("cameraOffset: " + cameraOffset);
        Vector3 tempV = CameraFoucsPoint.transform.position;
        if (player.playScene == PlayerBase.PlayScene.river)
        {
            transform.position = new Vector3(tempV.x + cameraOffset.x, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(tempV.x + cameraOffset.x/2, oriCameraPosition.y, transform.position.z);
        }
        Camera.main.orthographicSize = cameraSize;
    }

    public void InitCamerainfo()
    {
        ph = FindObjectOfType<PlayerHealth>();
        if (ph)
        {
            ph.GetNewCameraInfo();
        }
        oriCameraPosition = transform.position;
        cameraOffset = transform.position - CameraFoucsPoint.transform.position;
        cameraSize = Camera.main.orthographicSize;
    }

    public void InitCamerainfo(PlayerBase Player)
    {
        player = Player;
        CameraFoucsPoint = player.gameObject;
        InitCamerainfo();
    }
}
