using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUW : MonoBehaviour
{
    Animator moneyAnimator;
    private PlayerController player;

    [Header("∆Ω“∆æ‡¿Î")]
    public float distance;
    [Header("∆Ω“∆ÀŸ∂»")]
    public float speed;
    [Header("ÕÊº“≥Â¥Ãæ‡¿Î")]
    public Vector2 rushVec2;
    public float rushSpeed;

    private int state;
    private bool isActive;

    private Vector2 target;
    private static Vector2 OriPositon;

    private void Awake()
    {
        OriPositon = transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        state = 0;
        moneyAnimator = GetComponent<Animator>();
        target = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && Input.GetButtonDown("Jump"))
        {
            interactWithPlayer();
        }

        if (state == 1)
        {
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                state++;
            }
            else
            {
                Vector2 tempV = transform.position;
                transform.position = new Vector2(Mathf.MoveTowards(tempV.x, target.x, speed * Time.deltaTime), tempV.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController player))
            {
                this.player = player;
                player.action = () =>
                {
                    player.BeginRush(player.transform.position + (Vector3)rushVec2, rushSpeed);
                    AudioManager.PlayUWmoneySound();
                };

                if (state % 2 == 0)
                {
                    isActive = true;
                    player.playerState = 2;
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exit Money");
            isActive = false;
            player.playerState = 1;
        }
    }

    public List<int> interactWithPlayer()
    {
        Debug.Log("Money interaction");
        if (state == 0)
        {
            Vector2 tempVec2 = transform.position + new Vector3(distance, 0.0f);
            target = tempVec2;
            moneyAnimator.SetTrigger("active");
        }
        else if (state == 2)
        {
            Invoke("Destory", 0.5f);
        }

        isActive = false;
        state++;

        List<int> para = new List<int>();
        para.Add(state);
        return para;
    }

    void Destory()
    {
        gameObject.SetActive(false);
        Invoke("recover", 3f);
    }

    void recover()
    {
        gameObject.SetActive(true);
        transform.position = OriPositon;
    }
}
