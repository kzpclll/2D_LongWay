using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceToStart : MonoBehaviour
{
    public float fadeTime = 2.0f; // 文本消失所需的时间

    private Text text; // 存储文本组件的引用

    void Start()
    {
        text = GetComponent<Text>(); // 获取文本组件
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FadeText());
        }
    }

    IEnumerator FadeText()
    {
        Color originalColor = text.color; // 获取文本原始颜色
        float elapsedTime = 0.0f; // 记录已经消失的时间

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime; // 更新已经消失的时间
            float alpha = 1.0f - (elapsedTime / fadeTime); // 计算透明度

            Color newColor = originalColor;
            newColor.a = alpha; // 设置新颜色

            text.color = newColor; // 更新文本颜色

            yield return null; // 等待一帧
        }
        text.enabled = false; // 将文本隐藏
        Destroy(gameObject);
    }
}
