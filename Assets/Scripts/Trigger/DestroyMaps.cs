using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMaps : MonoBehaviour
{
    public GameObject[] objectsToDestroy; // 需要删除的物体数组

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 如果玩家触发了碰撞器
        {
            foreach (GameObject obj in objectsToDestroy) // 遍历需要删除的物体数组
            {
                Destroy(obj); // 删除每个物体
            }
        }
    }
}
