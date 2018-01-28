using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public static AudioSourcePool Instance { get; private set; }

    private readonly List<AudioSource> _audioSources = new List<AudioSource>();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    public void Play(AudioClip audioClip)
    {
        foreach (var audioSource in _audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip);
                return;
            }
        }

        var audioSourceContainer = new GameObject();
        audioSourceContainer.transform.parent = transform;
        audioSourceContainer.AddComponent<AudioSource>();
        var newAudioSource = audioSourceContainer.GetComponent<AudioSource>();
        newAudioSource.PlayOneShot(audioClip);
        _audioSources.Add(newAudioSource);
    }
}