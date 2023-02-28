using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float sprintSpeed = 4.5f;
    [SerializeField] private float sprintTime = 3f;
    [Range(0.00f, 0.3f)]
    [SerializeField] private float turnSmoothTime = 0.01f;
    private float currentMoveSpeed;

    [Header("Player Stats")]
    public int healthPoints;
    [Space(5)]
    public int candies = 0;
    [Space(5)]
    [SerializeField] private float invulTime;
    public bool isDead;

    [Header("Mode")]
    public bool infMode;
    public int candiesForWin = 6;
    public bool isWin;

    private Camera cam;
    private CharacterController controller;
    private AudioManager am;
    private Animator anim;

    private Vector3 moveDirection;

    private float targetAngle;
    private float turnSmoothVelocity;

    private float sprintTimeDelta;

    private float invulTimeDelta = 0f;

    private const float threshold = 0.01f;

    private void Awake()
    {
        Instance = this;

        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        am = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        am.ChangeVolume(1f);

        if (cam == null)
        {
            cam = Camera.main;
        }

        Candy.OnCandyCollected += CollectCandy;
        BulletCollider.OnPlayerHitted += Hitted;

        sprintTimeDelta = sprintTime;
        currentMoveSpeed = moveSpeed;
    }

    private void Start()
    {
        infMode = GameManager.Instance.mode == Mode.Infinity ? true : false;
    }

    private void Update()
    {
        invulTimeDelta -= Time.deltaTime;

        if (!isDead && !isWin)
        {
            Move();
        }
    }

    private void Move()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

        if(sprintTimeDelta > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            sprintTimeDelta -= Time.deltaTime;
            currentMoveSpeed = sprintSpeed;
        }
        else if(sprintTimeDelta < sprintTime && !Input.GetKey(KeyCode.LeftShift))
        {
            sprintTimeDelta = sprintTime;
            currentMoveSpeed = moveSpeed;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }

        if (moveDirection.magnitude >= threshold)
        {
            anim.SetBool("isMoving", true);

            targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(moveDirection * currentMoveSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void Die()
    {
        isDead = true;
        am.ChangeVolume(0f);
        am.PlaySound("death");
    }

    private void Hitted()
    {
        if (invulTimeDelta <= 0f)
        {
            healthPoints--;
            am.PlaySound("hit");

            invulTimeDelta = invulTime;

            if (healthPoints <= 0 && !isDead && !isWin)
            {
                Die();
            }
        }
    }

    private void CollectCandy()
    {
        candies++;
        am.PlaySound("eat");

        if (candies == candiesForWin && !isDead && !infMode)
        {
            isWin = true;
            am.ChangeVolume(0f);
            am.PlaySound("win");
        }
    }
}
