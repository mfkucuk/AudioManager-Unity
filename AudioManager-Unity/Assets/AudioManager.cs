using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        PlayMusic("music");
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
    public void PlayMusic(string musicName)
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
                _audioSource.Play();

                break;
            }
        }
    }

    /**
     * <summary>
     * This function should only be used when playing music.
     * </summary>
     */
    public void StopMusic()
    {
        _audioSource.Stop();
        _audioSource.clip = null;
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
