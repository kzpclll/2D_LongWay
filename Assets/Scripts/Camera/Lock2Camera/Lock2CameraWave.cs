using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock2CameraWave : MonoBehaviour
{
    public bool isLock;
    public GameObject FoucsPoint;
    public Vector2 offset;

    void Start()
    {
        LockOn();
    }

    void LateUpdate()
    {
        if (isLock)
        {
            transform.position = new Vector2(FoucsPoint.transform.position.x - offset.x, transform.position.y);
        }
    }

    public void LockOn()
    {
        offset = FoucsPoint.transform.position - transform.position;
        isLock = true;
    }

    public void LockOff()
    {
        isLock = false;
    }
    public void PlayheartWaveSound()
    {
        AudioManager.PlayheartWaveSound();
    }
}