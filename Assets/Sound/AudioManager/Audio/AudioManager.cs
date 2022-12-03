using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        PlaySound("Theme");
    }

    private void Update()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound: " + name + "not found!");
            return;
        }

        s.source.volume = s.volume;
        s.source.Play();
    }

    public float ClipLength(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound: " + name + "not found!");
            return 0;
        }
        return s.clip.length;
    }

    public Sound FindSound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                return s;
            }
        }

        return null;
    }

    public void ChangeVolumeTheme(float volume)
    {
        FindSound("Theme").volume = volume;
    }

    public void ChangeVolume(float volume)
    {
        FindSound("hit").volume = volume;
        FindSound("eat").volume = volume;
    }
}
