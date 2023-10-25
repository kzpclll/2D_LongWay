using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBezier: ItemBase
{
    //public List<Vector2> poses;
    public Vector2[] poses;
    float t;
    // for GetBezierPoint
    float a1, a2, a3, a4;
    Vector2[] res;
    // for DeCasteljau
    Vector2[] points = new Vector2[4];
    float argument1;
    float argument2;

    internal bool isTriggerActicve;

    public float speed;
    public bool isRotate;
    public bool isAutoRecover;
    public float AutoRecoverTime;
    public bool isMove;
    [Header("声音的顺序")]
    public int soundCount = 0;
    // 1=trainTriggerSound 2=trainSound 3=chainSound 4=puffSound


    // Start is called before the first frame update
    void Start()
    {
        isTriggerActicve = true;
        isMove = false;
        t = 0;
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            if (t > 1)
            {
                isMove = false;
                t = 0;
                if (isAutoRecover)
                {
                    Invoke("Recover", AutoRecoverTime);
                    AudioManager.PlayTriggerSound(soundCount);
                    // Debug.Log("ME");
                }
                else
                {
                    isTriggerActicve = true;
                    GetNewTransfrom();
                }
            }
            else
            {
                t += speed * Time.deltaTime;
                var res = GetBezierPoint3(t, poses[0], poses[1], poses[2], poses[3]);
                //transform.position = oriPosition + res[0];
                transform.position = oriPosition + DeCasteljau(t, poses);



                if (isRotate)
                {
                    Vector2 spdV = res[1].normalized;
                    float angle1 = Vector2.Angle(Vector2.up, spdV);
                    if (spdV.x < 0)
                    {
                        angle1 = -angle1;
                    }
                    transform.rotation = Quaternion.Euler(0, 0, -angle1);
                }
            }
        }
    }

    /// <summary>
    /// 使用三阶Bezier进行插值
    /// </summary>
    public Vector2[] GetBezierPoint3(float t, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        res = new Vector2[2];

        a1 = Mathf.Pow((1 - t), 3);
        a2 = 3 * t * (1 - t) * (1 - t);
        a3 = 3 * t * t * (1 - t);
        a4 = t * t * t;

        
        res[0] = a1 * p1 + a2 * p2 + a3 * p3 + a4 * p4;

        Vector2 v1 = p2 - p1;
        Vector2 v2 = p4 - p3;
        res[1] = Vector2.Lerp(v1, v2, t);

        return res;
    }

    public Vector2 DeCasteljau(float t,Vector2[] poses)
    {
        points = (Vector2[])poses.Clone();
        argument1 = t;
        argument2 = 1-t;
        int j = points.Length-1;
        while (j != 0)
        {
            for(int i = 0; i < j; i++)
            {
                points[i] = argument2 * points[i] + argument1 * points[i + 1];
            }
            j--;
        }
        return points[0];
    }

    public override void Recover()
    {
        transform.position = oriPosition;
        transform.rotation = oriRotation;
        isTriggerActicve = true;
    }

    public override void Recover2Awake()
    {
        if (t < 1.01f && t > 0.1f)
        {
            Debug.Log("recover but 0 < t < 1");
            t = 1.01f;
            StartCoroutine("LateAwake");
            return;
        }
        transform.position = awakePosition;
        transform.rotation = awakeRotation;
        GetNewTransfrom();
    }

    public IEnumerator LateAwake()
    {
        // Debug.Log("LateAwake");
        yield return 1;
        Recover2Awake();
    }
}
