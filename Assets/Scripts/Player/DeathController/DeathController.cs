using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public static DeathController instance;

    public Vector2 checkPointPosition;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                // do nothing
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
