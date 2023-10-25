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

        // 等待加载完成
        while (!asyncLoad.isDone)
        {
            // 获取加载进度
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // 在这里可以显示加载进度条或其他UI提示

            yield return null;
        }
    }
}


