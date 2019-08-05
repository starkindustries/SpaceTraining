using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // Singleton pattern
    // https://gamedev.stackexchange.com/a/116010/123894
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    public Sound[] sounds;

    private void Awake()
    {
        // Singleton Enforcement Code
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);

        // AudioMAnager Code
        foreach (Sound sound in sounds)
        {
            // Add a new AudioSource component for each playable sound        
            sound.SetSource(gameObject.AddComponent<AudioSource>());
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.Play();
    }
}