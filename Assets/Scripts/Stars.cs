using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 10f;// 自转速度
    public float minRotationAcceleration = 10f;
    public float maxRotationAcceleration = 30f;
    public float accelerationDuration = 0.5f;
    

    [Header("Object A Settings")]
    public GameObject player;

    public float gravityScaleInSpace = 0f;


    private float accelerationEndTime;
    private float currentAcceleration;
    private bool accelerating;

    private bool isRotating = false; // 是否正在加速旋转
    public float rotateAngle = 45f; // 加速旋转的角度
    public float rotateSpeedFactor = 2f; // 加速旋转的速度因子，值越大加速越快

    private bool timerActive = false; // 计时器是否生效
    public float maxHoldDuration = 1f; // 最大持续时间
    public float minHoldDuration = 0.5f; // 最小持续时间
    private float holdTimer = 0f; // 持续时间计时器

    private bool isCollided = false;
    private float lastLeaveTime;
    public float fadeDuration = 1.0f; // 淡入/淡出时间
    public float minAlpha = 0.2f; // 最小alpha值
    private SpriteRenderer spriteRenderer; // 渲染器组件


    private Vector3 startPosition;

    private void Update()
    {
        ProcessRotation();
        ProcessMouseClick();
        // 自转
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        // 加速旋转
        if (Input.GetMouseButtonDown(0) && !timerActive)
        {
            isRotating = true;
            timerActive = true;
            holdTimer = 0f;
        }
        if (isRotating)
        {
            transform.Rotate(0f, 0f, rotateAngle * rotateSpeedFactor * Time.deltaTime);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (holdTimer >= minHoldDuration) // 添加最小持续时间的限制
            {
                isRotating = false;
                timerActive = false;
                holdTimer = 0f;
            }
        }

        if (isCollided && player.transform.parent == null)
        {
            Animator playerAnimator = player.GetComponentInChildren<Animator>();
            playerAnimator.Play("isFlying");
        }

        if (isCollided && player.transform.parent != transform)
        {
            StartCoroutine(FadeOut());
            StartCoroutine(DisableCollider());
            isCollided = false;
            player.GetComponent<Rigidbody2D>().gravityScale = gravityScaleInSpace;
        }

        // 计时器
        if (timerActive)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= maxHoldDuration)
            {
                isRotating = false;
                timerActive = false;
                holdTimer = 0f;
            }
        }
        // 恢复星星透明度
        if (Time.time - lastLeaveTime > 5.0f && spriteRenderer.color.a < 1.0f)
        {
            StartCoroutine(FadeIn());
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;

    }
    private void ProcessRotation()
    {
        if (accelerating && Time.time > accelerationEndTime)
        {
            currentAcceleration = 0f;
            accelerating = false;
        }

        float currentRotationSpeed = rotationSpeed + currentAcceleration;
        transform.Rotate(Vector3.forward * currentRotationSpeed * Time.deltaTime);
    }

    private void ProcessMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentAcceleration = Random.Range(minRotationAcceleration, maxRotationAcceleration);
            accelerationEndTime = Time.time + accelerationDuration;
            accelerating = true;
        }
    }
    IEnumerator FadeIn()
    {
        Color color = spriteRenderer.color;
        float alpha = spriteRenderer.color.a;

        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime / fadeDuration;
            if (alpha > 1.0f) alpha = 1.0f;
            color.a = alpha;
            spriteRenderer.color = color;
            yield return null;
        }
        lastLeaveTime = 0.0f;
    }
    // 淡出效果
    IEnumerator FadeOut()
    {
        Color color = spriteRenderer.color;
        float alpha = 1.0f;

        while (alpha > minAlpha)
        {
            alpha -= Time.deltaTime / fadeDuration;
            if (alpha < minAlpha) alpha = minAlpha;// 添加alpha值最小值的限制
            color.a = alpha;
            spriteRenderer.color = color;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player && !isCollided)
        {
            Animator playerAnimator = player.GetComponentInChildren<Animator>();
            playerAnimator.Play("isAround");
            player.transform.SetParent(transform);
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;        
            Vector2 direction = collision.transform.position - transform.position;// 计算出玩家和星星之间的向量
            collision.transform.up = -direction.normalized;// 将玩家的Y轴朝向星星的中心
            player.GetComponentInParent<PlayerKid>().DoNothing();
            lastLeaveTime = 0.0f;
            isCollided = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player && isCollided)
        {
            lastLeaveTime = Time.time;
        }
    }
    IEnumerator DisableCollider()
    {
        // 禁用碰撞体
        GetComponent<Collider2D>().enabled = false;

        // 5秒后恢复碰撞体
        yield return new WaitForSeconds(5.0f);

        GetComponent<Collider2D>().enabled = true;
    }
}