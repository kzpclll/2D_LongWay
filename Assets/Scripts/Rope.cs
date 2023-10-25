using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    //��Ҫ���
    public HingeJoint2D hingeJoint { get; private set; }
    public DistanceJoint2D distanceJoint { get; private set; }

    [SerializeField] private float collingTime; //��ȴʱ��
    private bool isColl = false;    //�Ƿ�������ȴ

    private PlayerController player;

    private void Start()
    {
        hingeJoint = GetComponent<HingeJoint2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
    }

    IEnumerator Colling(float time)
    {
        isColl = true;
        yield return new WaitForSeconds(time);
        player.playerState = 1;
        isColl = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isColl && collision.gameObject.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController player))
            {
                this.player = player;
                Vector3 v = player.transform.position;
                v.x = transform.position.x;
                player.transform.position = v;
                distanceJoint.enabled = true;

                player.action = () =>
                {
                    distanceJoint.enabled = false;
                    player.Jump();
                    StartCoroutine(Colling(collingTime));
                };
                player.playerState = 2;
            }
        }
    }
}
