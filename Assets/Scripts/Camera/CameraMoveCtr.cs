using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveCtr : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    public bool MoveFlag = false;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveFlag = true;
        rb.velocity = Vector2.right * MoveSpeed; 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
