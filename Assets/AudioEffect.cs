using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioEffect : MonoBehaviour
{
    public bool playOnEnable = true;
    public float randomPitch = 0.0f;
    private float defaultPitch = 1.0f;
    private AudioSource source;

    void Awake() {
        source = GetComponent<AudioSource>();
        defaultPitch = source.pitch;
    }

    void OnEnable()
    {
        if(playOnEnable) Play();
    }

    public void Play() {
        source.pitch = defaultPitch + Random.Range(-randomPitch, randomPitch);
        source.Play();
    }
}
