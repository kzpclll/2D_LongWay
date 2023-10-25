using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UnderWaterOut : MonoBehaviour
{
    public PlayerController player;
    public Lock2Camera backGround;
    private PlayableDirector timeline;
    private Camera02 camera02;
    public FishTrack fish;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MainCamera").TryGetComponent<Camera02>(out camera02);
        timeline = this.GetComponentInParent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OutWater0");
        if (collision.gameObject.CompareTag("Player"))
        {
            timeline.Play();
            Debug.Log("OutWater1");
            player.initPlayScene(PlayerController.PlayScene.animation);

            backGround.LockOff();
            
            fish.isTrack = false;
            
            
        }
        
    }

    public void EndTimeLine()
    {
        Debug.Log("OutWater2");
        timeline.Stop();
        player.playerAnimator.Play("Base Layer.Wait");
        player.playerAnimator.SetBool("Swim", false);

        player.initPlayScene(PlayerController.PlayScene.normal);
        camera02.InitCamerainfo();
    }
}
