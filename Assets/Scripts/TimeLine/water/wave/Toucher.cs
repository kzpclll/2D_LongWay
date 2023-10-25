using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toucher : MonoBehaviour
{
    public GameObject Player;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Blocker"))
        {
            Player.GetComponent<PlayerController99>().isDragPlayerDown = false;
            Player.GetComponent<PlayerController99>().rb.velocity = new Vector2(0, 0);
            Player.GetComponent<PlayerController99>().isRowing = false;
        }
    }
}
