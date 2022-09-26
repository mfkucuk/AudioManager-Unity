using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Music[] _musicList;

    [SerializeField]
    private SoundEffect[] _soundEffectList;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        // Reset all the music.
        foreach (Music music in _musicList)
        {
            music.ResetCurrentTime();
        }

        PlaySoundEffect("testSound");
    }

    /**
     * <summary>
     * This function should only be used when playing music.
     * <paramref name="audioName"/> is the name of the sound effect to be played.
     * </summary>
     */
    public bool PlaySoundEffect(string audioName)
    {
        foreach (SoundEffect soundEffect in _soundEffectList)
        {
            if (audioName.Equals(soundEffect.audioName))
            {
                // Play the audio source
                _audioSource.PlayOneShot(soundEffect.audioClip, soundEffect.volume);
                return true;
            }
        }

        return false;
    }

    /**
     * <summary>
     * This function should only be used when playing music.
     * <paramref name="musicName"/> is the name of the music to be played.
     * </summary>
     */
    public bool PlayMusic(string musicName, bool resume)
    {
        foreach (Music music in _musicList)
        {
            if (musicName.Equals(music.audioName))
            {
                // Assign the necessary information from the scriptable object
                _audioSource.clip = music.audioClip;
                _audioSource.volume = music.volume;
                _audioSource.pitch = music.pitch;
                _audioSource.loop = music.isLooping;
                _audioSource.playOnAwake = music.isPlayedOnAwake;

                // Play the audio source
                if (resume)
                {
                    // Load the time
                    _audioSource.PlayScheduled(music.LoadCurrentTime());
                }
                else
                {
                    music.ResetCurrentTime();
                    _audioSource.Play();
                }
                return true;
            }
        }

        return false;
    }

    /**
     * <summary>
     * This function should only be used when playing music.
     * <paramref name="musicName"/> is the name of the music to be played.
     * </summary>
     */
    public bool StopMusic(string musicName)
    {
        foreach (Music music in _musicList)
        {
            if (musicName.Equals(music.audioName))
            {
                // Save the time
                music.SaveCurrentTime(_audioSource.time);

                // Stop the audio source
                _audioSource.Stop();
                return true;
            }
        }

        return false;
    }
}
