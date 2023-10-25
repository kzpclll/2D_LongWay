using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBezierTrigger : MonoBehaviour
{
    public Collider2D bc2d;
    [Header("ÉùÒôµÄË³Ðò")]
    public int soundCount = 0;
    // 1=trainTriggerSound 2=trainSound 3=chainSound 4=puffSound
    private void Awake()
    {
        bc2d = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var el = this.GetComponentInParent<CustomBezier>();
            if (el.isTriggerActicve)
            {
                el.isMove = true;
                el.isTriggerActicve = false;
                AudioManager.PlayTriggerSound(soundCount);
            }
        }
    }
}
