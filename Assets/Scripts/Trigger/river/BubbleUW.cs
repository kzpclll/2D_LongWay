using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleUW : MonoBehaviour
{
    public PlayerController player;

    public Vector2 rushVec2;
    public float rushSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController player))
            {
                player.playerState = 2;
                this.player = player;
                player.action = () =>
                {
                    player.BeginRush(player.transform.position + (Vector3)rushVec2, rushSpeed);
                    AudioManager.PlaypopSound();
                    AudioManager.PlayUWcoinSound();
                };
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController player))
            {
                player.playerState = 1;
            }
        }
    }
}
