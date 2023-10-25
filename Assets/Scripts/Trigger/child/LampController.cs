using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    public static LampController instance;

    private void Awake()
    {
        if (LampController.instance == null)
        {
            LampController.instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void sendMsg2Lamps()
    {
        foreach (Transform child in instance.transform)
        {
            if (child.CompareTag("light") && child.gameObject.activeInHierarchy)
            {
                child.GetComponent<Lamps>().OpenLamp();
                
            }
        }
    }
}
