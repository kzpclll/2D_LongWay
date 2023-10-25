using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 10f;// ��ת�ٶ�
    public float minRotationAcceleration = 10f;
    public float maxRotationAcceleration = 30f;
    public float accelerationDuration = 0.5f;
    

    [Header("Object A Settings")]
    public GameObject player;

    public float gravityScaleInSpace = 0f;


    private float accelerationEndTime;
    private float currentAcceleration;
    private bool accelerating;

    private bool isRotating = false; // �Ƿ����ڼ�����ת
    public float rotateAngle = 45f; // ������ת�ĽǶ�
    public float rotateSpeedFactor = 2f; // ������ת���ٶ����ӣ�ֵԽ�����Խ��

    private bool timerActive = false; // ��ʱ���Ƿ���Ч
    public float maxHoldDuration = 1f; // ������ʱ��
    public float minHoldDuration = 0.5f; // ��С����ʱ��
    private float holdTimer = 0f; // ����ʱ���ʱ��

    private bool isCollided = false;
    private float lastLeaveTime;
    public float fadeDuration = 1.0f; // ����/����ʱ��
    public float minAlpha = 0.2f; // ��Сalphaֵ
    private SpriteRenderer spriteRenderer; // ��Ⱦ�����


    private Vector3 startPosition;

    private void Update()
    {
        ProcessRotation();
        ProcessMouseClick();
        // ��ת
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        // ������ת
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
            if (holdTimer >= minHoldDuration) // �����С����ʱ�������
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

        // ��ʱ��
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
        // �ָ�����͸����
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
    // ����Ч��
    IEnumerator FadeOut()
    {
        Color color = spriteRenderer.color;
        float alpha = 1.0f;

        while (alpha > minAlpha)
        {
            alpha -= Time.deltaTime / fadeDuration;
            if (alpha < minAlpha) alpha = minAlpha;// ���alphaֵ��Сֵ������
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
            Vector2 direction = collision.transform.position - transform.position;// �������Һ�����֮�������
            collision.transform.up = -direction.normalized;// ����ҵ�Y�ᳯ�����ǵ�����
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
        // ������ײ��
        GetComponent<Collider2D>().enabled = false;

        // 5���ָ���ײ��
        yield return new WaitForSeconds(5.0f);

        GetComponent<Collider2D>().enabled = true;
    }
}