using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CheckPoint : MonoBehaviour
{
    private PlayerHealth ph;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerHealth>(out ph))
            {
                ph.checkPointPosition = this.transform.position + new Vector3(0,0.0f);
                // Debug.Log("change checkPoint : " + this.name);
            }
                
        }
    }
}
