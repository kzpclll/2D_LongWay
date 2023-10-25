using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private Vector3 savePosition;
    private PlayerKid pk;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // ����ҽӴ����浵��ʱ��������ҵ�λ��
            savePosition = collision.gameObject.transform.position;
            Debug.Log("�浵��");
        }
    }
    public void UpdateSavePoint(PlayerKid playerKid)
    {
        playerKid.savePoint = this;
    }

    public Vector3 GetSavePosition()
    {
        return savePosition;
    }
}
