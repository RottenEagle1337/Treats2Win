using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [Header("Pause")]
    public bool isPaused;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        PauseGame();
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f;
            MenuManager.Instance.OpenMenu("pause");
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;

            if (PlayerController.Instance.isDead)
                MenuManager.Instance.OpenMenu("dead");
            else
                MenuManager.Instance.OpenMenu("hp");
        }
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (PlayerController.Instance.isDead)
            MenuManager.Instance.OpenMenu("dead");
        else
            MenuManager.Instance.OpenMenu("hp");
        
    }
}
