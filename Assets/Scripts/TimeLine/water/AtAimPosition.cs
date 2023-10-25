using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


/*
 * 该脚本主要用于控制在海上划船阶段的Timeline状态
 * 该函数中的脚本有trigger触发的，也有被其它函数调用的
 */
public class AtAimPosition : MonoBehaviour
{
    PlayerControllerBow pcb;
    private GameObject stoppers;
    public InitWaveController IWC;


    public PlayableDirector[] timelines = new PlayableDirector[2];
    public int[] waves = new int[2];
    // 0=等待进行互动 1=timeline互动中 2=timeline互动完毕
    // 等那边传值，自己不维护timelineState
    public int timelineState = 0;
    private int currentTimelineCount = 0;
    private bool isRunning;
    // Start is called before the first frame update
    void Start()
    {
        stoppers = this.transform.Find("Stoppers").gameObject;
        timelines[0] = this.transform.Find("timeline1").gameObject.GetComponent<PlayableDirector>();
        timelines[1] = this.transform.Find("timeline2").gameObject.GetComponent<PlayableDirector>();
        timelineState = 0;
        currentTimelineCount = 0;
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //借助该物体的collider，停止当前timeline，切换pcb模式
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("aap get player");
            if (!isRunning)
            {
                IWC.toNextTimeline();
                isRunning = true;
                collision.gameObject.TryGetComponent<PlayerControllerBow>(out pcb);
                // 外部player模式切换 collider启动
                if (pcb)
                {
                    stoppers.SetActive(true);
                    pcb.ChangeMode(true);
                }
            }
        }
    }

    // 停止当前timeline，如果还有timeline，则进入下一个timeline
    public void StopNowTimeline()
    {
        Debug.Log("stop timeline..");
        timelines[currentTimelineCount].Stop();
        if(timelines.Length > currentTimelineCount + 1)
        {
            Debug.Log("start new timeline..");
            currentTimelineCount++;
            activeTimeline();
        }
        else
        {
            Debug.Log("no more timeline..");
        }
    }

    public void activeTimeline()
    {
        IWC.InitTimelineInfo(waves[currentTimelineCount]);
        timelines[currentTimelineCount].Play();
    }

    public void ResetTimeline()
    {
        StartCoroutine("LateReset");
    }

    IEnumerator LateReset()
    {
        timelines[currentTimelineCount].Stop();
        yield return new WaitForSeconds(6);
        timelines[currentTimelineCount].Play();
    }
}
