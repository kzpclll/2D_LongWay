using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;

    public enum PlayScene
    {
        normal,
        river,
        boat,
        animation
    }

    [HideInInspector]
    public PlayScene playScene;
    [HideInInspector]
    public bool isPlayerMove;
    [HideInInspector]
    public Action action;
    [HideInInspector]
    public int playerState;
    [HideInInspector]
    public bool jumpPressed; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action()
    {
        return;
    }
}
