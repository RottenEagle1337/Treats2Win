using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private void Start()
    {
        Slider volumeSlider = GetComponentInChildren<Slider>();
        volumeSlider.value = AudioManager.instance.FindSound("Theme").volume;
    }

    public void SetVolume(float volume)
    {
        AudioManager.instance.ChangeVolumeTheme(volume);
    }
}
