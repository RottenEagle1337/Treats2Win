using UnityEngine;
using UnityEngine.UI;

public class Candy : MonoBehaviour
{
    public Material[] options;
    private Renderer planeRenderer;

    private void Awake()
    {
        planeRenderer = GetComponentInChildren<Renderer>();

        planeRenderer.material = options[Random.Range(0, options.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController target = other.GetComponent<PlayerController>();
            if (target != null)
            {
                Debug.Log("Player collect candy");
                target.CollectCandy();
            }
            Destroy(gameObject);
        }
    }
}
