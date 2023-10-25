using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lamps : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isReadyOpen;
    private float step;
    private float value;
    [Header("�ƹ��ģʽ")]
    public LampMode lampMode;
    [Header("�ر�״̬����ɫColor")]
    public Color closeColor;
    [Header("����״̬����ɫColor")]
    public Color openColor;
    [Header("�ƹ��ʼǿ��Light2D")]
    public float intensity0;
    [Header("�ƹ��ʼǿ��Light2D")]
    public float intensity1;
    [Header("�仯ʱ��")]
    public float changeTime;
    public GameObject objToDelete;

    private UnityEngine.Rendering.Universal.Light2D light2d;
    public enum LampMode
    {
        color,
        light2D
    }
    void Start()
    {
        step = 1/changeTime;
        sr = GetComponent<SpriteRenderer>();
        isReadyOpen = false;
        if (lampMode == LampMode.color)
        {
            GetComponent<SpriteRenderer>().color = closeColor;
        }else if(lampMode == LampMode.light2D)
        {
            light2d = transform.GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
            if (light2d)
            {
                light2d.intensity = intensity0;
            }
            else
            {
                Debug.LogError("Error From :" + gameObject.name);
                Debug.LogError("û����lamp�����������ҵ�Light2D��");
            }
        }
        else
        {
            Debug.LogError("��Ч��lampMode");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isReadyOpen = true;
            
        }
    }

    public void OpenLamp()
    {
        if (isReadyOpen)
        {
            if (lampMode == LampMode.color)
            {
                StartCoroutine("OpenLampByColor"); 
                AudioManagerC.PlayLampAudio();
            }
            else if(lampMode == LampMode.light2D)
            {
                StartCoroutine("OpenLampByLight2d");
            }
            else
            {
                Debug.LogError("��Ч��lampMode");
            }
        }
    }

    IEnumerator OpenLampByColor()
    {
        value = Mathf.MoveTowards(sr.color.a, 1, step*Time.fixedDeltaTime);
        sr.color = new Color(255, 255, 255, value);
        if (value < 1)
        {
            yield return new WaitForFixedUpdate();
            StartCoroutine("OpenLampByColor");
        }
        else
        {
            Debug.Log("OpenLampByColor over");
            isReadyOpen = false;
        }
    }

    IEnumerator OpenLampByLight2d()
    {
        value = Mathf.MoveTowards(light2d.intensity, intensity1, step * Time.fixedDeltaTime);
        light2d.intensity = value;
        if (value < intensity1)
        {
            yield return new WaitForFixedUpdate();
            StartCoroutine("OpenLampByLight2d");
        }
        else
        {
            Debug.Log("OpenLampByLight2d over");
            isReadyOpen = false;
            Destroy(objToDelete);
        }
    }
}
