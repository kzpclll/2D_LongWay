using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 返回指定位置的接口
/// </summary>
public class ItemBase : MonoBehaviour
{
    public Vector2 oriPosition;
    public Quaternion oriRotation;

    internal Vector2 awakePosition;
    internal Quaternion awakeRotation;
    // Start is called before the first frame update

    private void Awake()
    {
        oriPosition = transform.position;
        oriRotation = transform.rotation;
        awakePosition = transform.position;
        awakeRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Recover()
    {
        transform.position = oriPosition;
        transform.rotation = oriRotation;
    }
    
    public virtual void Recover2Awake()
    {
        transform.position = awakePosition;
        transform.rotation = awakeRotation;
        GetNewTransfrom();
    }

    public virtual void GetNewTransfrom()
    {
        oriPosition = transform.position;
        oriRotation = transform.rotation;
    }
}
