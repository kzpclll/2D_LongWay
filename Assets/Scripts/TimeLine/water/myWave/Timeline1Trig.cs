using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline1Trig : MonoBehaviour
{
    private AtAimPosition aap;
    // Start is called before the first frame update
    void Start()
    {
        aap = GetComponentInParent<AtAimPosition>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        aap.activeTimeline();
    }
}
