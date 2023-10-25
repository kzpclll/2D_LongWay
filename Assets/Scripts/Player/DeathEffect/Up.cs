using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : MonoBehaviour
{
    public PlayerController99 pc99;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("down"))
        {
            if (GetComponentInParent<DeathEffect>().estate == DeathEffect.effectState.close)
            {
                GetComponentInParent<DeathEffect>().wait();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("down"))
        {
            Debug.Log("DeathEffect kill player");
            pc99.getHit();
        }
    }
}
