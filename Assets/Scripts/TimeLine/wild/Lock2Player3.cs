using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock2Player3 : MonoBehaviour
{
    public Transform player2;
    public bool isLock;
    // Start is called before the first frame update
    void Start()
    {
        isLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLock)
        {
            transform.position = player2.transform.position;
        }
    }

    public void Lock2Camera()
    {
        isLock = true;
    }

    public void Unlock2Camera()
    {
        isLock = false;
    }
}
