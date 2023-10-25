using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UnderwaterIn : MonoBehaviour
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
        timeline = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginTimeline()
    {
        
    }

    public void EndTimeline()
    {
        timeline.Stop();
        Debug.Log("Underwater2");
        player.initPlayScene(PlayerController.PlayScene.river);

        player.playerAnimator.Play("Base Layer.Swim");
        player.playerAnimator.SetBool("Swim", true);

        backGround.LockOn();
        camera02.cameraOffset = camera02.transform.position - player.transform.position;
        fish.isTrack = false;
        camera02.InitCamerainfo();

        // player.transform.localScale = new Vector3 (-player.transform.localScale.x, player.transform.localScale.y , player.transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Underwater1");
            timeline.Play();
            player.initPlayScene(PlayerController.PlayScene.animation);
        }
    }
}
