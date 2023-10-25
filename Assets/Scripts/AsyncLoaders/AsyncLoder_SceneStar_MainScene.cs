using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoder_SceneStar_MainScene : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad; // �� Unity �༭��������Ҫ���صĳ�������

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
