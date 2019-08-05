using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;

    private AudioSource source;

    public void SetSource(AudioSource newSource)
    {
        // Copy the sound's properties to the new audio source
        source = newSource;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
    }

    public void Play()
    {
        Debug.Log("Playing: " + name);
        source.Play();
    }
}
