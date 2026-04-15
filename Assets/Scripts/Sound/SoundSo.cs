using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundSo", menuName = "Scriptable Objects/SoundSo")]
public class SoundSo : ScriptableObject
{
    public AudioClip audioClip;
    [Range(0f, 2f)]
    public float volume = 1f;
    [Range(-3f, 3f)]
    public float pitch = 1f;
    public bool loop = false;
    public bool randomizePitch = false;
    [Range(0f, 1f)]
    public float pitchRange = 0.1f;
    
}
