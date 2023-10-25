using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPend : MonoBehaviour
{
    PlayerBase player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCamera()
    {
        Camera.main.GetComponent<Camera02>().InitCamerainfo(player);
    }
}
