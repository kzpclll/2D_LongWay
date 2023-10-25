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
    /// 本脚本负责：初始化当前场景的Timeline，并与单例的EscMenuController沟通，从而播放指定的Timeline达到切换关卡的效果
    /// 该脚本还包括一些对EscMenuController的控制
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
        // 控制ESC菜单 不想再多添加脚本所以放在这里
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

    // 随时被菜单调用
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

    // 等start结束，大多数参数初始化结束后，播放timeline
    IEnumerator Wait2PlayTimeline()
    {
        yield return new WaitForEndOfFrame();
        EscMenuController.instance.ChangePageByIndex(0);
        EscMenuController.instance.gameObject.SetActive(false);
        PlayTimelineAwake();
    }
}
