using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ChildPortal : MonoBehaviour
{
    public Transform childPortalOut;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = childPortalOut.position;

            // ����ָ���� timeline ����
            PlayableDirector director = FindObjectOfType<PlayableDirector>();
            if (director != null && director.state == PlayState.Playing)
            {
                // ֹͣ��ǰ�� timeline ���������²���
                director.Stop();
                director.time = 0f;
                director.Play();
            }
        }
    }
}
