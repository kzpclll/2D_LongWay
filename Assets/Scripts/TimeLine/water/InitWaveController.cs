using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 该脚本中的函数控制浪的生成，包括生成浪的prefab，浪的数量，生成模式
 * 该脚本中的函数主要通过timeline被动调用
 */

public class InitWaveController : MonoBehaviour
{
    [Header("选择初始化位置与初始化Prefab")]
    public Transform generatePositionWave;
    public Transform generatePositionbottle;

    [Header("输入目标浪数量")]
    private int aimWaveCount;

    [Header("手动选择同目录下的AtAimPositon")]
    public AtAimPosition AtAimPosition;

    [Header("Timeline的模式")]
    public timelineMode mode;

    public enum timelineMode { 
        waveCount,
        trigger,
        once
    }
    public timelineMode[] modes = {timelineMode.trigger,timelineMode.once};
    public GameObject[] gameobjs;

    [Header("不设置")]
    public bool isNext = false;
    private int waveCount;
    // state=0等待互动 state=1正在互动 state=2完成此阶段互动 
    private int state;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        waveCount = 0;
        aimWaveCount = 2;
    }

    // signal调用该函数实现浪的生成
    public void InitWave(int num)
    {
        if (mode == timelineMode.waveCount)
        {
            if (aimWaveCount == 0)
            {
                Debug.Log("Wave Controller : aimWaveCount=0,可能是因为没有正确初始化。");
            }

            if (waveCount < aimWaveCount)
            {
                state = 1;
                Debug.Log("Wave Controller : genertate wave..");
                waveCount++;
                GenerateObj(num);
            }
            else
            {
                state = 2;
                Debug.Log("Wave Controller : genertate number max..");
            }
        }else if (mode == timelineMode.trigger || mode == timelineMode.once)
        {
            GenerateObj(num);
        }

    }

    // 每个循环的末尾，signal调用该函数来判断是否切换timeline
    public void CheckState()
    {
        Debug.Log("check state = " + state);
        if (state == 2) {
            AtAimPosition.StopNowTimeline();
            mode = modes[1];
            state = 0;
        }
    }

    // 外部trigger调用该函数实现对state的改变，从而切换timeline
    public void toNextTimeline()
    {
        state = 2;
    }

    public void InitTimelineInfo(int wave)
    {
        this.state = 0;
        this.waveCount = 0;
        aimWaveCount = wave;
    }


    // 0:wave 1~4 bottle 11~14 special bottle
    public void GenerateObj(int num)
    {
        Debug.Log("GenerateObj:" + num);
        switch(num){
            case 0:
                GameObject.Instantiate(gameobjs[0], generatePositionWave);
                break;

            case 1:
                GameObject.Instantiate(gameobjs[1], generatePositionbottle);
                break;

            case 2:
                GameObject.Instantiate(gameobjs[2], generatePositionbottle);
                break;

            case 3:
                GameObject.Instantiate(gameobjs[3], generatePositionbottle);
                break;

            case 4:
                GameObject.Instantiate(gameobjs[4], generatePositionbottle);
                break;
            case 5:
                GameObject.Instantiate(gameobjs[5], generatePositionbottle);
                break;

            default:
                Debug.LogError("generateObj get error number");
                break;
        }
           
    }
}
