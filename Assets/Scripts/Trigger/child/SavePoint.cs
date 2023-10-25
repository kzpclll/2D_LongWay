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
            // 当玩家接触到存档点时，保存玩家的位置
            savePosition = collision.gameObject.transform.position;
            Debug.Log("存档了");
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
