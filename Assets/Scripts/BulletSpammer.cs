using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpammer : MonoBehaviour
{
    [Header("Timer Settings")]
    public Vector2 firstWaveTimerRange = new Vector2(5f, 10f);
    public Vector2 timerRange = new Vector2(10f, 40f);

    [Header("Waves settings")]
    public float waveCooldown = 4f;
    [SerializeField] GameObject prefab;
    [SerializeField] private List<Wave> waves;

    private float currentTimer = 0f;
    private float timer = 0f;

    private Animator anim;
    private void Start()
    {
        timer = Random.Range(firstWaveTimerRange.x, firstWaveTimerRange.y);
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= timer)
        {
            StartCoroutine(StartWave());
            timer = Random.Range(timerRange.x, timerRange.y);
            currentTimer = 0f;
        }
    }

    IEnumerator StartWave()
    {

        for (int i = 0; i < waves.Count; i++)
        {
            anim.SetTrigger("SpawnBullet");
            float angle = 360 / waves[i].quantity;

            for (int j = 0; j < waves[i].quantity; j++)
            {
                Quaternion rotation = Quaternion.Euler(0, angle, 0);

                GameObject bullet = Instantiate(prefab, transform.position, rotation, transform);

                BulletCollider bulletData = bullet.GetComponent<BulletCollider>();
                bulletData.SetSpeed(waves[i].speed);
                //bulletData.SelLoopLength(waves[i].trajectoryLoopLength);
                bulletData.SetCurve(waves[i].trajectory);

                angle += (360 / waves[i].quantity);
            }
            yield return new WaitForSeconds(waveCooldown);
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, radius);
    }
}
