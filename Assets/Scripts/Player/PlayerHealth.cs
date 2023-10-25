using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool isDie;
    private Animator anim;
    public Vector2 checkPointPosition;
    private PlayerBase player;
    public float dieTime;
    public CameraBase camera2;
    public HealthManager hm;

    private float cameraHeight;
    private void Start()
    {
        hm = GetComponentInParent<HealthManager>();
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerBase>();
        isDie = false;
        cameraHeight = Camera.main.orthographicSize;
        camera2 = Camera.main.GetComponent<CameraBase>();
    }

    private void LateUpdate()
    {
        // ³¬³öÆÁÄ»ËÀÍö
        if (player.playScene != PlayerBase.PlayScene.animation && isDie!=true)
        {
            if (Mathf.Abs(player.transform.position.y - Camera.main.transform.position.y) > cameraHeight)
            {
                Debug.Log("Fall Out!");
                Damaged();
            }
        }


    }

    public void Damaged()
    {
        if (!isDie)
        {
            isDie = true;
            anim.SetBool("isDie", true);
            player.isPlayerMove = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Invoke("KillPlayer", dieTime);
            AudioManager.PlaydeathVoice();
        }
    }

    public void deathVoice()
    {
        AudioManager.PlaydeathVoice();
    }

    void KillPlayer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Invoke("PlayerRespawn", dieTime);
    }

    void PlayerRespawn()
    {
        transform.position = checkPointPosition;
        player.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        camera2.CameraFresh();
        anim.SetBool("isDie", false);
        player.isPlayerMove = true;
        player.jumpPressed = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<SpriteRenderer>().enabled = true;
        isDie = false;
        hm.PlayerDead();
        AudioManager.PlayRespawnVoice();
    }

    public void GetNewCameraInfo()
    {
        cameraHeight = Camera.main.orthographicSize;
    }
}