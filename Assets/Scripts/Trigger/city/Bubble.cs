using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameObject.layer = 7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision enter bubble");

        animator.SetTrigger("activate");
        if(collision.gameObject.transform.tag == "Player")
        {
            // 2 = bubble
            collision.gameObject.GetComponent<PlayerController>().playerState = 2;
        }
        Invoke("Destory", 0.3f);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("collision exit bubble");
        if (collision.gameObject.transform.tag == "Player")
        {
            // 1 = ground
            collision.gameObject.GetComponent<PlayerController>().playerState = 1;
        }
    }




    void Destory()
    {
        gameObject.SetActive(false);
        Invoke("recover", 5f);
    }

    void recover()
    {
        gameObject.SetActive(true);
    }
}
