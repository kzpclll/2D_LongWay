using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float coolDown=1.5f;

    private AudioSource sound;
    private bool isActive;
    public int index = -1;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isActive)
        {
            GameObject other = collision.gameObject;
            if (other.GetComponentInParent<PlayerKid>())
            {
                if (other.GetComponentInParent<PlayerKid>().isDown)
                {
                    sound.Play();
                    isActive = false;
                    PianoController.instance.GetKey(index);
                    animator.SetTrigger("isDown");
                    StartCoroutine("CoolDown");
                }
            }

        }
    }

    IEnumerator CoolDown()
    {
        isActive = false;
        yield return new WaitForSeconds(coolDown);
        isActive = true;
    }

    public void GetIndex(int index)
    {
        this.index = index;
    }

    public void EnableHint()
    {
        animator.SetBool("isFlash",true);
    }

    public void DisableHint()
    {
        animator.SetBool("isFlash", false);
    }
}
