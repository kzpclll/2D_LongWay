using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceToStart : MonoBehaviour
{
    public float fadeTime = 2.0f; // �ı���ʧ�����ʱ��

    private Text text; // �洢�ı����������

    void Start()
    {
        text = GetComponent<Text>(); // ��ȡ�ı����
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
        Color originalColor = text.color; // ��ȡ�ı�ԭʼ��ɫ
        float elapsedTime = 0.0f; // ��¼�Ѿ���ʧ��ʱ��

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime; // �����Ѿ���ʧ��ʱ��
            float alpha = 1.0f - (elapsedTime / fadeTime); // ����͸����

            Color newColor = originalColor;
            newColor.a = alpha; // ��������ɫ

            text.color = newColor; // �����ı���ɫ

            yield return null; // �ȴ�һ֡
        }
        text.enabled = false; // ���ı�����
        Destroy(gameObject);
    }
}
