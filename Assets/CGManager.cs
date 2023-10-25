using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject money;
    public Camera01 camera01;

    public enum GameMode
    {
        normal,
        gameplay,
        dialogueMoment
    }

   

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MainCamera").TryGetComponent<Camera01>(out camera01);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void Begin()
    {
        Debug.Log("Begin!");
        player.isPlayerMove = false;
        player.rb.bodyType = RigidbodyType2D.Static;
        player.rb.gravityScale = 0; 
    }
    public void Event1()
    {
        Debug.Log("1");
        player.sr.enabled = false;
        money.SetActive(false);
    }


    public void Event2()
    {
        Debug.Log("CG 2");
        //camera01.transform.position = GameObject.Find("CameraPosition2").transform.position;
        camera01.transform.position = new Vector3(361.96f,-13f,-5f);
        Debug.Log("camera Position:" + camera01.transform.position);

    }

    public void Event3()
    {
        Debug.Log("3");
        player.sr.enabled = true;
        player.isPlayerMove = true;
        player.rb.bodyType = RigidbodyType2D.Dynamic;

        player.changePlayScene(2);
    }

    public void Finish()
    {

        Debug.Log("Finish!");
    }
}
