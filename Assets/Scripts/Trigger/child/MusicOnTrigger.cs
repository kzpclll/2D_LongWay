using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnTrigger : MonoBehaviour
{
    public AudioClip musicClip; // ���ּ���
    public AudioSource musicSource; // ���ֲ�����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �����Ҵ�������ײ��
        {
            Debug.Log("��������");
            musicSource.clip = musicClip; // �������ּ���
            musicSource.loop = true; // ����Ϊѭ������
            musicSource.Play(); // ��ʼ��������
        }
    }
}
