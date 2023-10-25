using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class EscMenuController : MonoBehaviour
{
    public static EscMenuController instance;

    GameObject[] pages;
    public int timelineIndex;
    public string sceneName;

    private void Awake()
    {
        // ����ģʽ�Ĳ˵�
        timelineIndex = -1;
        if (EscMenuController.instance == null)
        {
            EscMenuController.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (EscMenuController.instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {

    }

    private void Start()
    {
        // ��ʼ���˵�ҳ��
        int childCount = transform.childCount;
        pages = new GameObject[childCount];
        for(int i = 0; i < childCount; i++)
        {
            pages[i] = transform.GetChild(i).gameObject;
            print(pages[i].name);
        }
    }

    public void StartNewStage(string sceneName, int timelineIndex)
    {
        // ��ʼ�¹ؿ�������timeline
        this.timelineIndex = timelineIndex;
        this.sceneName = sceneName;
        if (SceneManager.GetActiveScene().name.Equals(sceneName))
        {
            GameObject.FindObjectOfType<TLPlayerBase>().PlayTimelineRuntime(timelineIndex);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }

    public void ChangePageByIndex(int index)
    {
        if(index < transform.childCount && index >= 0)
        {
            int count = 0;
            foreach(GameObject page in pages)
            {
                page.SetActive(count==index);
                count++;
            }
        }
        else
        {
            Debug.LogError("�л�ҳ��ʱ�����˴����index��");
        }
    }

    public void ChangePage_ChooseStage()
    {
        transform.Find("Page0").gameObject.SetActive(false);
        transform.Find("Page_chooseStage").gameObject.SetActive(true);
    }

    public void ChangePage_back2Main()
    {
        transform.Find("Page0").gameObject.SetActive(true);
        transform.Find("Page_chooseStage").gameObject.SetActive(false);
    }


}
