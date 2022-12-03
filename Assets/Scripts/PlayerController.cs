using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Mobile Input")]
    [SerializeField] private Joystick joystick;

    [Header("Movement")]
    [Tooltip("Move speed of the character in m/s")]
    public float moveSpeed = 3f;
    [Tooltip("Sprint force of the character in N")]
    public float sprintSpeed = 4.5f;
    [Tooltip("Time required to pass before being able to sprint again. Set to 0f to instantly sprint again")]
    public float sprintTimeout = 3f;
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.00f, 0.3f)]
    public float turnSmoothTime = 0.01f;

    [Header("Player Stats")]
    public int healthPoints;
    [SerializeField] private TMP_Text hpCounter;
    [Space(5)]
    public int candies = 0;
    [SerializeField] private TMP_Text candyCounter;
    [Space(5)]
    public float invulTime;
    public bool isDead;

    [Header("Mode")]
    public bool infMode;
    public int candiesForWin = 6;
    public bool isWin;

    private GameObject cam;
    private Rigidbody rb;
    private AudioManager audio;

    float zMove;
    float xMove;
    Vector2 direction;

    float targetAngle;
    float turnSmoothVelocity;

    float sprintTimeoutDelta = 0f;
    float sprintForce;

    float invulTimeDelta = 0f;

    private const float threshold = 0.01f;
    private const float gravity = -9.81f;

    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody>();

        audio = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        audio.ChangeVolume(1f);

        if (cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera");
        }

        sprintForce = 5f * rb.mass * (sprintSpeed - moveSpeed) / Time.fixedDeltaTime;

        Cursor.visible = false;

        infMode = SettingsManager.Instance.mode == Mode.Infinity ? true : false;
    }

    void Update()
    {
        sprintTimeoutDelta -= Time.deltaTime;
        invulTimeDelta -= Time.deltaTime;

        hpCounter.text = healthPoints.ToString();
        candyCounter.text = candies.ToString();

        if (!isDead && !isWin)
        {
            Move();
            //Sprint();
        }
    }

    void Move()
    {
        zMove = joystick.Vertical; //Input.GetAxis("Vertical");
        xMove = joystick.Horizontal; //Input.GetAxis("Horizontal");
        direction = new Vector2(xMove, zMove).normalized;

        if (direction.magnitude >= threshold)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }

    public void Sprint()
    {
        if (sprintTimeoutDelta < 0) //&& Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(transform.forward * sprintForce);
            sprintTimeoutDelta = sprintTimeout;
        }
    }

    void Die()
    {
        MenuManager.Instance.OpenMenu("dead");
        audio.ChangeVolume(0f);
        audio.PlaySound("Dead");
    }

    public void Hitted()
    {
        if (invulTimeDelta <= 0f)
        {
            healthPoints--;
            audio.PlaySound("hit");
            invulTimeDelta = invulTime;
            if (healthPoints <= 0 && !isDead && !isWin)
            {
                isDead = true;
                Die();
            }
        }
    }

    public void CollectCandy()
    {
        candies++;
        audio.PlaySound("eat");
        if (candies == candiesForWin && !isDead && !infMode)
        {
            MenuManager.Instance.OpenMenu("win");
            isWin = true;
            audio.ChangeVolume(0f);
            audio.PlaySound("Win");
        }
    }
}
