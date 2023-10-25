using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


/*
 * �ýű���Ҫ���ڿ����ں��ϻ����׶ε�Timeline״̬
 * �ú����еĽű���trigger�����ģ�Ҳ�б������������õ�
 */
public class AtAimPosition : MonoBehaviour
{
    PlayerControllerBow pcb;
    private GameObject stoppers;
    public InitWaveController IWC;


    public PlayableDirector[] timelines = new PlayableDirector[2];
    public int[] waves = new int[2];
    // 0=�ȴ����л��� 1=timeline������ 2=timeline�������
    // ���Ǳߴ�ֵ���Լ���ά��timelineState
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

    //�����������collider��ֹͣ��ǰtimeline���л�pcbģʽ
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
                // �ⲿplayerģʽ�л� collider����
                if (pcb)
                {
                    stoppers.SetActive(true);
                    pcb.ChangeMode(true);
                }
            }
        }
    }

    // ֹͣ��ǰtimeline���������timeline���������һ��timeline
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
