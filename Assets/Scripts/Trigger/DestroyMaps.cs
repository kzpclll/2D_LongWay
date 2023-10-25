using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMaps : MonoBehaviour
{
    public GameObject[] objectsToDestroy; // ��Ҫɾ������������

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �����Ҵ�������ײ��
        {
            foreach (GameObject obj in objectsToDestroy) // ������Ҫɾ������������
            {
                Destroy(obj); // ɾ��ÿ������
            }
        }
    }
}
