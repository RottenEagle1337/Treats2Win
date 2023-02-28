using System;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    public static Action OnPlayerHitted;

    public Material[] options;
    private Renderer planeRenderer;

    private float speed;
    private float length;
    private float timer = 0f;

    private bool setCurve = false;

    private Rigidbody rb;
    private AnimationCurve ac;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        planeRenderer = GetComponentInChildren<Renderer>();
        planeRenderer.material = options[UnityEngine.Random.Range(0, options.Length)];
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
        rb.velocity = transform.forward * speed;
    }

    public void SetCurve(AnimationCurve _ac)
    {
        ac = _ac;
        setCurve = true;
    }

    public void SelLoopLength(float value)
    {
        length = value;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (setCurve)
        {
            transform.position = transform.position + transform.right * ac.Evaluate(timer) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
            return;
        else if (other.CompareTag("Player"))
            OnPlayerHitted.Invoke();
        Destroy(gameObject);
    }

}
