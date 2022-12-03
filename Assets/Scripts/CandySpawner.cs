using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public List<Transform> positions;
    public Vector2 waveDuration;
    public int minCandyCount;

    public bool infMode;

    private List<GameObject> candies = new List<GameObject>();

    private float timer;
    private float currentWaveDuration;
    private float currentCandyCount;

    void Start()
    {
        infMode = SettingsManager.Instance.mode == Mode.Infinity ? true : false;

        if (!infMode)
        {
            for (int i = 0; i < PlayerController.Instance.candiesForWin; i++)
            {
                int n = Random.Range(0, positions.Count - 1);
                Instantiate(prefab, positions[n].position + new Vector3(0f, 0.6f, 0f), Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), transform);
                positions.RemoveAt(n);
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if ((infMode && !PlayerController.Instance.isDead) && timer >= currentWaveDuration)
        {
            timer = 0f;
            currentWaveDuration = Random.Range(waveDuration.x, waveDuration.y);

            if (candies.Count > 0)
            {
                for (int j = 0; j < candies.Count; j++)
                {
                    Destroy(candies[j]);
                }
                candies.Clear();
            }

            currentCandyCount = Random.Range(minCandyCount, positions.Count);

            for (int i = 1; i <= currentCandyCount; i++)
            {
                int n = Random.Range(0, positions.Count - i);

                GameObject candy = Instantiate(prefab, positions[n].position + new Vector3(0f, 0.6f, 0f), Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), transform);
                candies.Add(candy);

                positions.Add(positions[n]);
                positions.RemoveAt(n);
            }
        }
    }
}
