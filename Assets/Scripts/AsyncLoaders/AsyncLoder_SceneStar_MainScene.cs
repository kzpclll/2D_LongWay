using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoder_SceneStar_MainScene : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad; // 在 Unity 编辑器中设置要加载的场景名称

    public void LoadScenes()
    {
        StartCoroutine(LoadNextSceneAsync());
    }
    IEnumerator LoadNextSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
