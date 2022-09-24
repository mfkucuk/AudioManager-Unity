using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sound")]
public class Sound : ScriptableObject
{
    public AudioClip audioClip;
    public string audioName;
    
    [Range(0f, 1f)]
    public float volume;

    [Range(-3f, 3f)]
    public float pitch;

    public bool isLooping = false;
    public bool isPlayedOnAwake = false;

    public float delay = 0;
}
