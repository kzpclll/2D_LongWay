using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DeadZone : MonoBehaviour
{
    public SavePoint point;
    public PlayerKid plkid;
    public PlayableDirector director;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            plkid.Re();
            if (point != null)
            {
                plkid.transform.position = point.GetSavePosition();
                plkid.playerAnimator.Play("����");
                Debug.Log("��������");
            }
            // ����ָ���� timeline ����
            if ( director.state == PlayState.Playing)
            {
                // ֹͣ��ǰ�� timeline ���������²���
                director.Stop();
                director.time = 0f;
                Debug.Log("ͣ������");
            }
        }
    }
}
