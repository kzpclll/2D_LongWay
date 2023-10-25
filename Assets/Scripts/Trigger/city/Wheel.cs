using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    Rigidbody2D PlayerRb;
    [Header("力度")]
    public float upPow;
    [Header("冷却时间")]
    public float coolDownTime;
    [Header("Layer")]
    public LayerMask aimLayer;
    private bool isCoolDown;
    PlayerController player;

    
    // Start is called before the first frame update
    void Start()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCoolDown)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
                {
                    bool wheelCheck = Physics2D.OverlapCircle(player.groundCheck.position, 0.05f, aimLayer);
                    Debug.Log("isNear" + wheelCheck);
                    if (wheelCheck)
                    {
                        CoolDown(coolDownTime);
                        player.rb.velocity = new Vector2(player.rb.velocity.x, upPow);

                        AudioManager.PlayWheelAudio();
                    }
                }

            }
            
        }

    }

}
