using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private SpriteRenderer sr;
    [Header("变化时间")]
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("Fade");
        }
    }

    IEnumerator Fade()
    {
        if (sr.color.a > 0.01f)
        {
            float value = Mathf.MoveTowards(sr.color.a, 0, (1 / time) * Time.deltaTime);
            sr.color = new Color(255, 255, 255, value);
            yield return new WaitForFixedUpdate();
            StartCoroutine("Fade");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
