using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Mode { Normal, Infinity };

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public Mode mode;

    public TMP_Dropdown modeDropdown;

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

    public void SetMode()
    {
        if (modeDropdown.value == 0)
            mode = Mode.Normal;
        else
        {
            mode = Mode.Infinity;
        }
    }
}
