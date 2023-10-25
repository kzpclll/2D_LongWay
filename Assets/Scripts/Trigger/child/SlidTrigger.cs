using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidTrigger : MonoBehaviour
{
    public PlayerKid player;
    public bool slidOn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             player.GetComponent<Rigidbody2D>().gravityScale = 1;
             player.transform.rotation = new Quaternion(0, 0, 0, 0);

            if (slidOn)
            {
                player.GetComponentInParent<PlayerKid>().BeginSlid();
            }
            else
            {
                player.GetComponentInParent<PlayerKid>().EndSlid();
            }
        }
    }
}
