using System;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public static Action OnCandyCollected;

    [SerializeField] private Material[] options;
    private Renderer planeRenderer;

    private void Awake()
    {
        planeRenderer = GetComponentInChildren<Renderer>();

        planeRenderer.material = options[UnityEngine.Random.Range(0, options.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCandyCollected.Invoke();
            Destroy(gameObject);
        }
    }

}
