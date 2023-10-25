using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChanger : MonoBehaviour
{
    [Header("自动搜索PlayerManager(不必填选)")]
    public PlayerManager pm;

    public enum changeMode{
        p1Top2,
        p2Top1
    };

    public changeMode cm;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (cm == changeMode.p1Top2)
            {
                pm.Player1Change2();
            }
            else if (cm == changeMode.p2Top1)
            {
                pm.Player2Change1();
            }
        }
    }
}
