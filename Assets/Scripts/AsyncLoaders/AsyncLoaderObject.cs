using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoaderObject : MonoBehaviour
{
    public static AsyncLoaderObject instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public void LoadSceneAsync(string targetSceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(targetSceneName));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string targetSceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);

        // �ȴ��������
        while (!asyncLoad.isDone)
        {
            // ��ȡ���ؽ���
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // �����������ʾ���ؽ�����������UI��ʾ

            yield return null;
        }
    }
}


