using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PianoController : MonoBehaviour
{
    [HideInInspector]
    public static PianoController instance;
    [Header("����")]
    public int[] music;
    private int musicIndex;
    [Header("Debug")]
    public Key[] keys;

    private bool isFinish = false;

    public UnityEngine.Playables.PlayableDirector timelineDirector;

    private void Awake()
    {
        if (PianoController.instance==null)
        {
            PianoController.instance = this;
        }
        else
        {
            // do nothing
        }

        for (int n =0;n < music.Length;n++)
        {
            music[n] = music[n] - 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PianoController.instance.keys = GetComponentsInChildren<Key>();
        GiveIndex2Children();
        musicIndex = 0;
        if (music.Length > musicIndex)
        {
            EnableHint(music[musicIndex]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ��������Ѿ�ȫ����ɣ��򲥷�timeline����
        if (isFinish)
        {
            timelineDirector.Play();
        }
    }
    public void DestroyTimelineQuit()
    {
        Destroy(timelineDirector.gameObject);
    }

    void DeStroyThis()
    {
        Destroy(gameObject);
    }

    void DisableHint(int index)
    {
        keys[index].DisableHint();
    }

    void EnableHint(int index)
    {
        keys[index].EnableHint();
    }

    //�����������
    public void GetKey(int index) {
        if (isFinish)
        {
            return;
        }


        if(index == -1)
        {
            Debug.LogError("PianoController��δ��ʼ����key");
            return;
        }
        else
        {
            if (index == music[musicIndex])
            {
                DisableHint(music[musicIndex]);
                musicIndex++;
                if(musicIndex < music.Length)
                {
                    EnableHint(music[musicIndex]);
                }
                else
                {
                    isFinish = true;
                    Debug.Log("�������");
                }
                
            }
            else
            {
                Debug.Log("�����ˣ����ø���");
                DisableHint(music[musicIndex]);
                musicIndex = 0;
                EnableHint(music[musicIndex]);
            }
        }
    }

    //�·��±�
    private void GiveIndex2Children()
    {
        int temp = 0;
        foreach(Key key in keys)
        {
            key.GetIndex(temp);
            temp++;
        }
    }
}
