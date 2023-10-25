using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Animator animator;
    bool isActive;

    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && Input.GetButtonDown("Jump"))
        {
            interactWithPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController player))
            {
                Debug.Log("player enter coin");
                this.player = player;
                player.action = () =>
                {
                    // player.Jump();
                    player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpVelocity);
                    AudioManager.PlaycoinSound();
                };

                player.playerState = 2;
                isActive = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController player))
            {
                //Debug.Log("player exit coin");
                player.playerState = 1;
                isActive = false;
            }
        }
    }


    public List<int> interactWithPlayer()
    {
        isActive = false;
        animator.SetTrigger("activate");
        
        Invoke("Destory", 0.4f);

        List<int> para = new List<int>();
        para.Add(0);
        return para;
    }
    void Destory()
    {
        gameObject.SetActive(false);
        Invoke("recover", 3f);
    }

    void recover()
    {
        gameObject.SetActive(true);
    }
}
