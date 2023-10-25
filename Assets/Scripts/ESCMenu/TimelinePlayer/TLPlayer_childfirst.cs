using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;

public class TLPlayer_childfirst : TLPlayerBase
{
    /// <summary>
    /// ���ű����𣺳�ʼ����ǰ������Timeline�����뵥����EscMenuController��ͨ���Ӷ�����ָ����Timeline�ﵽ�л��ؿ���Ч��
    /// �ýű�������һЩ��EscMenuController�Ŀ���
    /// </summary>
    private void Awake()
    {

    }

    private void Start()
    {
        StartCoroutine(Wait2PlayTimeline());
    }

    private void Update()
    {
        // ����ESC�˵� �����ٶ���ӽű����Է�������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscMenuController.instance.gameObject.SetActive(!EscMenuController.instance.gameObject.activeSelf);
        }
    }

    public override void Initialize()
    {
        return;
    }

    // awake 
    public override void PlayTimelineAwake()
    {
        if (EscMenuController.instance.timelineIndex==-1)
        {
            timelines[defaultTimelineIndex].Play();
        }
        else if(timelines[EscMenuController.instance.timelineIndex])
        {
            timelines[EscMenuController.instance.timelineIndex].Play();
        }
        else
        {
            Debug.Log("Error Timeline of Awake");
        }

        return;
    }

    // ��ʱ���˵�����
    public override void PlayTimelineRuntime(int index)
    {
        if (timelines[index])
        {
            timelines[index].Play();
        }
        else
        {
            Debug.Log("Error Timeline of Runtime");
        }

        return;
    }

    // ��start�����������������ʼ�������󣬲���timeline
    IEnumerator Wait2PlayTimeline()
    {
        yield return new WaitForEndOfFrame();
        EscMenuController.instance.ChangePageByIndex(0);
        EscMenuController.instance.gameObject.SetActive(false);
        PlayTimelineAwake();
    }
}
