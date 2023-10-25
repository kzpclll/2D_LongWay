using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager current;

    [Header("��Ч����")]
    public AudioClip whooshSound;
    public AudioClip swishSound;
    public AudioClip popSound;
    public AudioClip puffSound;
    


    [Header("������Ч")]
    public AudioClip wheelSound;
    public AudioClip coinSound;
    public AudioClip moneySound;
    public AudioClip balloonSound;
    public AudioClip umbrellaSound;
    public AudioClip umbrellaSound2;
    public AudioClip UWcoinSound;
    public AudioClip UWmoneySound;
    public AudioClip UWmovementSound;
    public AudioClip trainTriggerSound;
    public AudioClip trainSound;
    public AudioClip chainSound;
    public AudioClip fishSound;
    public AudioClip flappingSound;

    public AudioClip[] grassSound;
    public AudioClip heartWaveSound;

    public AudioClip paperBoatSound;
    public AudioClip paperCollision;
    public AudioClip pillsSound;
    public AudioClip bottleSound;
    public AudioClip bottlePillShake;
    public AudioClip seaWaveSound;
    public AudioClip splashUmbrella;


    [Header("��ɫ����")]
    public AudioClip[] jumpSound;//��
    public AudioClip deathVoice;//����
    public AudioClip respawnVoice;//����
    public AudioClip[] openUmbrellaSound;//��ɡ
    public AudioClip[] AccelerateSound;//����



    [Header("�����б���ѡ��")]
    public AudioSource ambientSource;
    public AudioSource musicSource;
    public AudioSource fxSource;
    public AudioSource voiceSource;
    public AudioSource playerSource;
    public AudioSource itemSource;
    public AudioSource itemSource2;

    AudioSource publicSource;
    private void Awake()
    {
        current = this;

        AudioSource[] audioArray = GetComponents<AudioSource>();

        ambientSource = audioArray[0];
        musicSource = audioArray[1];
        fxSource = audioArray[2];
        voiceSource = audioArray[3];
        playerSource = audioArray[4];
        itemSource = audioArray[5];
        itemSource2 = audioArray[6];

        publicSource = gameObject.AddComponent<AudioSource>();
        StartLevelAudio();
    }

    void StartLevelAudio()
    {

    }

    public static void PlayJumpAudio()//��Ծ
    {
        int index = Random.Range(0, current.jumpSound.Length);

        current.playerSource.clip = current.jumpSound[index];
        current.playerSource.Play();

    }
    public static void PlaydeathVoice()//������Ч
    {
        current.voiceSource.clip = current.deathVoice;
        current.voiceSource.Play();
    }
    public static void PlayRespawnVoice()//����
    {
        current.voiceSource.clip = current.respawnVoice;
        current.voiceSource.Play();
    }

    public static void PlayWheelAudio()//��̥
    {
        current.itemSource.clip = current.wheelSound;
        current.itemSource.Play();
    }
    public static void PlaycoinSound()//Ӳ��
    {
        current.itemSource.clip = current.coinSound;
        current.itemSource.Play();
    }

    public static void PlaymoneySound()//ֽ��
    {
        current.itemSource.clip = current.moneySound;
        current.itemSource.Play();

        current.fxSource.clip = current.whooshSound;
        current.fxSource.Play();
    }

    public static void PlayballoonSound()//����
    {
        current.itemSource.clip = current.balloonSound;
        current.itemSource.Play();

        current.fxSource.clip = current.swishSound;
        current.fxSource.Play();
    }

    public static void PlayumbrellaSound()//ɡ
    {
        current.playerSource.clip = current.umbrellaSound;
        current.playerSource.Play();
    }
    public static void PlayumbrellaSound2()//����ɡ��������
    {
        current.itemSource.clip = current.umbrellaSound2;
        current.itemSource.Play();
    }
    public static void PlaypopSound()//��������
    {
        current.fxSource.clip = current.popSound;
        current.fxSource.Play();
    }

    public static void PlaywhooshSound()//������
    {
        current.fxSource.clip = current.whooshSound;
        current.fxSource.Play();
    }
    public static void PlayswishSound()//����
    {
        current.fxSource.clip = current.swishSound;
        current.fxSource.Play();
    }
    public static void PlayUWcoinSound()//ˮ��Ӳ����
    {
        current.itemSource.clip = current.UWcoinSound;
        current.itemSource.Play();
    }
    public static void PlayUWmoneySound()//ˮ��ֽ����
    {
        current.itemSource2.clip = current.UWmoneySound;
        current.itemSource2.Play();

        current.fxSource.clip = current.UWmovementSound;
        current.fxSource.Play();
    }
    public static void PlaytrainTriggerSound()//�𳵴���
    {
        current.ambientSource.clip = current.trainTriggerSound;
        // current.ambientSource.Play();
        current.ambientSource.PlayOneShot(current.ambientSource.clip);

        current.musicSource.clip = current.trainSound;
        // current.musicSource.Play();
        current.musicSource.PlayOneShot(current.musicSource.clip);
    }
    public static void PlayopenUmbrellaSound()//��ɡ
    {
        int index = Random.Range(0, current.openUmbrellaSound.Length);

        current.playerSource.clip = current.openUmbrellaSound[index];
        current.playerSource.Play();
    }
    public static void PlaygrassSound()//«έ����
    {
        int index = Random.Range(0, current.grassSound.Length);

        current.itemSource.clip = current.grassSound[index];
        current.itemSource.Play();
    }
    public static void PlayheartWaveSound()//�ĵ纣����
    {
        current.itemSource.clip = current.heartWaveSound;
        current.itemSource.Play();
    }
    public static void PlayAccelerateSound()//���ٻ���
    {
        int index = Random.Range(0, current.AccelerateSound.Length);

        current.voiceSource.clip = current.AccelerateSound[index];
        current.voiceSource.Play();
    }

    public static IEnumerator PlayDelayedSound(AudioSource source, AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.PlayOneShot(clip);
    }

    public static void PlayWaveObjSound(int soundCount)
    {
        switch (soundCount)
        {
            case 1:
                current.publicSource.PlayOneShot(current.paperBoatSound);
                current.itemSource2.PlayOneShot(current.paperCollision);
                break;
            case 2:
                current.publicSource.PlayOneShot(current.bottleSound);
                current.itemSource2.PlayOneShot(current.bottlePillShake);
                break;
            case 3:
                current.publicSource.PlayOneShot(current.seaWaveSound);
                current.itemSource2.PlayOneShot(current.splashUmbrella);
                break;
            case 4:
                current.publicSource.PlayOneShot(current.pillsSound);
                break;
            default:
                break;
        }

    }
    public static void PlayTriggerSound(int soundCount)
    {
        switch (soundCount)
        {
            case 1:
                current.publicSource.PlayOneShot(current.trainTriggerSound);
                current.voiceSource.PlayOneShot(current.trainSound);
                break;
            case 2:
                current.fxSource.PlayOneShot(current.fishSound);
                break;
            case 3:
                current.ambientSource.PlayOneShot(current.chainSound);
                break;
            case 4:
                current.fxSource.PlayOneShot(current.puffSound);
                break;
            case 5:
                current.itemSource2.PlayOneShot(current.flappingSound);
                break;
            default:
                break;
        }

    }
}
