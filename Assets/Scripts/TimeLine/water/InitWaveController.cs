using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �ýű��еĺ��������˵����ɣ����������˵�prefab���˵�����������ģʽ
 * �ýű��еĺ�����Ҫͨ��timeline��������
 */

public class InitWaveController : MonoBehaviour
{
    [Header("ѡ���ʼ��λ�����ʼ��Prefab")]
    public Transform generatePositionWave;
    public Transform generatePositionbottle;

    [Header("����Ŀ��������")]
    private int aimWaveCount;

    [Header("�ֶ�ѡ��ͬĿ¼�µ�AtAimPositon")]
    public AtAimPosition AtAimPosition;

    [Header("Timeline��ģʽ")]
    public timelineMode mode;

    public enum timelineMode { 
        waveCount,
        trigger,
        once
    }
    public timelineMode[] modes = {timelineMode.trigger,timelineMode.once};
    public GameObject[] gameobjs;

    [Header("������")]
    public bool isNext = false;
    private int waveCount;
    // state=0�ȴ����� state=1���ڻ��� state=2��ɴ˽׶λ��� 
    private int state;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        waveCount = 0;
        aimWaveCount = 2;
    }

    // signal���øú���ʵ���˵�����
    public void InitWave(int num)
    {
        if (mode == timelineMode.waveCount)
        {
            if (aimWaveCount == 0)
            {
                Debug.Log("Wave Controller : aimWaveCount=0,��������Ϊû����ȷ��ʼ����");
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

    // ÿ��ѭ����ĩβ��signal���øú������ж��Ƿ��л�timeline
    public void CheckState()
    {
        Debug.Log("check state = " + state);
        if (state == 2) {
            AtAimPosition.StopNowTimeline();
            mode = modes[1];
            state = 0;
        }
    }

    // �ⲿtrigger���øú���ʵ�ֶ�state�ĸı䣬�Ӷ��л�timeline
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
