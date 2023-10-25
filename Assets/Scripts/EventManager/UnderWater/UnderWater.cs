using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWater : MonoBehaviour
{
    public bool setEye;
    public bool setFish;

    public FishTrack ft;
    public EyeAnimation ea;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            setValue(setEye, setFish);
        }
    }

    private void setValue(bool setEye,bool setFish)
    {
        ft.isTrack = setFish;
        ea.aniamtor.SetBool("isEyeOpen", setEye);
        ea.aniamtor.SetBool("isEyeClose", !setEye);
    }
}
