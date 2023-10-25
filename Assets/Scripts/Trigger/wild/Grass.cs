using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] private float collingTime; //冷却时间
    private bool isColl = false;    //是否正在冷却
    public Vector2 pow;
    public Animator ani;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    IEnumerator Colling(float time)
    {
        isColl = true;
        yield return new WaitForSeconds(time);
        isColl = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isColl && collision.gameObject.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerBase>(out PlayerBase player))
            {
                ani.SetTrigger("Active");
                player.rb.velocity = new Vector2(player.rb.velocity.x+pow.x, pow.y);

                StartCoroutine(Colling(collingTime));
                
            }
        }
    }
    public void PlaygrassSound()
    {
        AudioManager.PlaygrassSound();
    }
}
