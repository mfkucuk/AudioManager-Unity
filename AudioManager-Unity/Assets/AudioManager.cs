using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] _soundList;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        PlaySoundEffect("testSound");
    }

    /**
     * Important info: this function should only be used when playing sound effects.
     * (such as jumping, hurting, dealing damage and so on.)
     * @param audioName: name of the sound effect to be played.
     */
    public bool PlaySoundEffect(string audioName)
    {
        foreach (Sound sound in _soundList)
        {
            if (audioName.Equals(sound.audioName))
            {
                // Play the audio source
                _audioSource.PlayOneShot(sound.audioClip, sound.volume);
                return true;
            }
        }

        return false;
    }

    /**
     * Important info: this function should only be used when playing music.
     * @param musicName: name of the music to be played.
     */
    public bool PlayMusic(string musicName)
    {
        foreach (Sound sound in _soundList)
        {
            if (musicName.Equals(sound.audioName))
            {
                // Assign the necessary information from the scriptable object
                _audioSource.clip = sound.audioClip;
                _audioSource.volume = sound.volume;
                _audioSource.pitch = sound.pitch;
                _audioSource.loop = sound.isLooping;
                _audioSource.playOnAwake = sound.isPlayedOnAwake;

                // Play the audio source
                _audioSource.Play();
                return true;
            }
        }

        return false;
    }

    public bool StopMusic(string audioName)
    {
        foreach (Sound sound in _soundList)
        {
            if (audioName.Equals(sound.audioName))
            {
                // Stop the audio source
                _audioSource.Stop();
                return true;
            }
        }

        return false;
    }
}
