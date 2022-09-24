using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] _soundList;

    private void Start()
    {
        foreach (Sound sound in _soundList)
        {
            
        }
    }

    public bool PlayAudio(string soundName)
    {
        foreach (Sound sound in _soundList)
        {
            if (soundName.Equals(sound.audioName))
            {
                
                return true;
            }
        }

        return false;
    }
}
