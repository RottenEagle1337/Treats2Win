using UnityEngine;

public enum Mode { Normal, Infinity };

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Mode mode;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        mode = Mode.Normal;
    }
}