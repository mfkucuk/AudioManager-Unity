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

        PlayMusic("music", true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlaySoundEffect("ah");
        }
    }

    /**
     * <summary>
     * This function should only be used when playing music.
     * <paramref name="audioName"/> is the name of the sound effect to be played.
     * </summary>
     */
    public void PlaySoundEffect(string audioName)
    {
        foreach (SoundEffect soundEffect in _soundEffectList)
        {
            if (audioName.Equals(soundEffect.audioName))
            {
                // Play the audio source
                _audioSource.PlayOneShot(soundEffect.audioClip, soundEffect.volume);

                break;
            }
        }
    }

    /**
     * <summary>
     * This function should only be used when playing music.
     * <paramref name="musicName"/> is the name of the music to be played.
     * </summary>
     */
    public void PlayMusic(string musicName, bool resume)
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

                // Play the audio source
                if (resume)
                {
                    // Load the time
                    _audioSource.PlayScheduled(music.LoadCurrentTime());
                }
                else
                {
                    Debug.Log("Why aren't you working?");
                    //music.ResetCurrentTime();
                    _audioSource.Play();
                }

                break;
            }
        }
    }

    /**
     * <summary>
     * This function should only be used when playing music.
     * <paramref name="musicName"/> is the name of the music to be played.
     * </summary>
     */
    public void StopMusic(string musicName)
    {
        foreach (Music music in _musicList)
        {
            if (musicName.Equals(music.audioName))
            {
                // Save the time
                music.SaveCurrentTime(_audioSource.time);

                // Stop the audio source
                _audioSource.Stop();

                break;
            }
        }
    }

    public void ChangeMusicVolume(string audioName, float newVolume)
    {
        foreach (Music music in _musicList)
        {
            if (audioName.Equals(music.audioName))
            {
                music.volume = newVolume;
                break;
            }
        }
    }
}
