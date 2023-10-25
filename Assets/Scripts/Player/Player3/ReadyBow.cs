using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyBow : MonoBehaviour
{
    private PlayerControllerBow pcb;
    private BoxCollider2D bx2d;
    private Animator ani;
    [Header("������")]
    public CameraBase cb;

    // Start is called before the first frame update
    void Start()
    {
        pcb = GetComponent<PlayerControllerBow>();
        bx2d = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        cb = Camera.main.GetComponent<Camera02>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // signal ���ô˺������������ҵ�������
    public void readyBow()
    {
        pcb.enabled = true;
        bx2d.isTrigger = false;
        ani.SetTrigger("isRowing");
        cb.InitCamerainfo(pcb);
    }
}
