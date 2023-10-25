using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//´¹Ö±µÄÌøÔ¾ÕÚÑôÉ¡
public class Umbrella : MonoBehaviour
{
    Animator animator;
    Rigidbody2D PlayerRb;
    public float upPow;
    private bool isCoolDown;
    public float coolDownTime;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        coolDownTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CoolDown(float time)
    {
        isCoolDown = true;
        yield return new WaitForSeconds(time);
        isCoolDown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCoolDown)
        {
            animator.SetTrigger("active");
            CoolDown(coolDownTime);
            PlayerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, upPow);
            AudioManager.PlayumbrellaSound();
            AudioManager.PlayswishSound();
        }
    }
}
