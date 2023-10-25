using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerC : MonoBehaviour
{
    public static AudioManagerC current;

    [Header("��Ч����")]
    public AudioClip[] lampSound;
    public AudioClip[] cryingSound;

    [Header("������Ч")]
    public AudioClip echoSound;

    [Header("�����б���ѡ��")]
    public AudioSource babySource;
    public AudioSource lampSource;
    public AudioSource echoSource;

    private void Awake()
    {
        current = this;
        AudioSource[] audioArray = GetComponents<AudioSource>();

        babySource = audioArray[0];
        lampSource = audioArray[1];


        echoSource = gameObject.AddComponent<AudioSource>();
        StartLevelAudio();
    }
    void StartLevelAudio()
    {

    }
      public static void PlayCryingAudio()//��
    {
        int index = Random.Range(0, current.cryingSound.Length);

        current.babySource.clip = current.cryingSound[index];
        current.babySource.Play();

    }  
    
      public static void PlayLampAudio()//���ص�
    {
        int index = Random.Range(0, current.lampSound.Length);

        current.lampSource.clip = current.lampSound[index];
        current.lampSource.Play();

    }



    void Update()
    {
        
    }
}
