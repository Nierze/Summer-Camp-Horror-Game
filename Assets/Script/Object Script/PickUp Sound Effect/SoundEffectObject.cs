using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume = 1f;
    public float pitch = 1f;
    public bool loop = false;
}

public class SoundEffectObject : MonoBehaviour
{
    public AudioSource medkitAudioSource;
    public AudioMixerGroup mixerGroup;
    public Sound[] sounds;

    private Dictionary<string, AudioSource> soundDictionary;

    private void Awake()
    {
        soundDictionary = new Dictionary<string, AudioSource>();

        foreach (var sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
            source.outputAudioMixerGroup = mixerGroup;

            soundDictionary[sound.name] = source;
        }
    }

    public void PlaySound(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out var source))
        {
            source.Play();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }
}
