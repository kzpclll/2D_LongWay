using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    Animator moneyAnimator;
    private PlayerController player;

    [Header("平移距离")]
    public float distance;
    [Header("平移速度")]
    public float speed;
    [Header("玩家冲刺力度")]
    public float powX;
    public float powY;
    

    private int state;
    private bool isActive;

    private Vector2 target;
    private Vector2 oriPositon;

    private void Awake()
    {
        oriPositon = transform.position;
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
                StartCoroutine(WaitToRecover());
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
                Debug.Log("Player" + player.name + "enter Money");
                this.player = player;
                player.action = () =>
                {
                    //player.Jump();
                    player.rb.velocity = new Vector2(powX, powY);
                    AudioManager.PlaymoneySound();
                };

                if (state % 2 == 0)
                {
                    Debug.Log("playerstate = 2");
                    isActive = true;
                    player.playerState = 2;
                    Debug.Log(player.playerState);
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController player))
            {
                //Debug.Log("player state = 1");
                player.playerState = 1;
                isActive = false;
            }
        }
    }

    public List<int> interactWithPlayer()
    {
        Debug.Log("Money interaction");
        if(state == 0)
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
        Debug.Log("money recover");
        gameObject.SetActive(true);
        transform.position = oriPositon;
        state = 0;
    }

    private IEnumerator WaitToRecover()
    {
        yield return new WaitForSeconds(2);
        if(state == 2)
        {
            Destory();
        }
    }
}
