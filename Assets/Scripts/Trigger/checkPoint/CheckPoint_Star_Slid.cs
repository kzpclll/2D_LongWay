using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint_Star_Slid : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DeathController.instance.checkPointPosition = transform.position;
            Debug.Log("arrive checkpoint");
        }
    }
}
