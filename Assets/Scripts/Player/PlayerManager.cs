using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* *
 * SampleSceneµƒPlayerManager
 * */
public class PlayerManager : MonoBehaviour
{
    public PlayerBase player1;
    public PlayerBase player2;
    public PlayerBase playerBow;

    [Header("≤ª…Ë÷√")]
    public CameraBase cb;
    // public PlayerBase player3;

    private GameObject mainPlayer;
    // Start is called before the first frame update
    void Start()
    {
        cb = Camera.main.GetComponent<Camera02>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Player1Change2() {
        if (player1.gameObject.activeSelf)
        {
            player1.gameObject.SetActive(false);
            player2.gameObject.SetActive(true);
            player2.transform.position = player1.transform.position;
            cb.InitCamerainfo(player2);
        }
    }

    public void Player2Change1()
    {
        if (player2.gameObject.activeSelf)
        {
            player1.gameObject.SetActive(true);
            player2.gameObject.SetActive(false);
            player1.transform.position = player2.transform.position;
            cb.InitCamerainfo(player1);
        }
    }

    public void Change2PlayerBow()
    {
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);

        playerBow.gameObject.SetActive(true);
        playerBow.transform.position = player2.transform.position;
        cb.InitCamerainfo(playerBow);
    }

    
}
