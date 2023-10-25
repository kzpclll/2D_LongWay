using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTrack : ItemBase
{
    public bool isTrack;
    public float trackSpeed;
    public float acc;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.Find("Player/Player_2").GetComponent<PlayerController>();
        isTrack = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    { 
        if (isTrack)
        {
            Vector2 playerP = player.transform.position;
            Vector2 fishP = transform.position;
            float dis = Vector2.Distance(playerP, fishP);

            float x = Mathf.MoveTowards(fishP.x, playerP.x, (Mathf.Max(dis * dis * acc, trackSpeed)) * Time.deltaTime);
            float y = Mathf.MoveTowards(fishP.y, playerP.y, (Mathf.Max(dis * dis * acc, trackSpeed)) * Time.deltaTime);
            transform.position = new Vector2(x,y);

            Vector2 spdV = new Vector2(playerP.x - fishP.x, 2 * (playerP.y - fishP.y)).normalized;
            float angle1 = Vector2.Angle(Vector2.up, spdV);

            if (playerP.x < fishP.x)
            {
                angle1 = -angle1;
            }

            transform.rotation = Quaternion.Euler(0, 0, -angle1);
        }
    }
}
