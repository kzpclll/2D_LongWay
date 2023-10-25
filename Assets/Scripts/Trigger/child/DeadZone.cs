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
                plkid.playerAnimator.Play("待机");
                Debug.Log("死翘翘了");
            }
            // 重置指定的 timeline 动画
            if ( director.state == PlayState.Playing)
            {
                // 停止当前的 timeline 动画并重新播放
                director.Stop();
                director.time = 0f;
                Debug.Log("停掉音乐");
            }
        }
    }
}
