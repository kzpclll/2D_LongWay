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

            // 重置指定的 timeline 动画
            PlayableDirector director = FindObjectOfType<PlayableDirector>();
            if (director != null && director.state == PlayState.Playing)
            {
                // 停止当前的 timeline 动画并重新播放
                director.Stop();
                director.time = 0f;
                director.Play();
            }
        }
    }
}
