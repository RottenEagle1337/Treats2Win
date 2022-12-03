using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCollider : MonoBehaviour
{
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
        planeRenderer.material = options[Random.Range(0, options.Length)];
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
        if (other.tag != "Bullet")
        {
            PlayerController target = other.GetComponent<PlayerController>();
            if (target != null)
            {
                target.Hitted();
            }

            Destroy(gameObject);
        }
    }
}
