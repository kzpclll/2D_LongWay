using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ð±ÏòÌøÔ¾ÕÚÑôÉ¡
public class Umbrella1 : MonoBehaviour
{
    Animator animator;
    Rigidbody2D PlayerRb;
    public float upPow;
    public float xPow;

    private float coolDownTime;
    private bool isCoolDown;
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

    IEnumerator CoolDown (float time)
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
            PlayerRb.velocity = new Vector2(PlayerRb.velocity.x + xPow, upPow);
            AudioManager.PlayumbrellaSound2();
            AudioManager.PlaywhooshSound();
        }
    }
}
