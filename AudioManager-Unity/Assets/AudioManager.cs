/*
 * This is an all purpose Audio Manager that can play music and sound effect separately.
 * Author: Mehmet Feyyaz K���k
 * 
 * TO-DO LIST:
 * - Play music and sound effect using (one)->two audio sources (DONE) (IMPROVED)
 * - Keep playing the music where it left off across scenes (DONE)
 * - Produce very basic sound effects using code (NOT DONE)
 * - Use an audio mixer to control the volume of all musics in the game (DONE)
 * - Change the volume of a music in the runtime over a period of time or immediately (DONE)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonnPersistent<AudioManager>
{
    [SerializeField]
    private Music[] _musicList;

    [SerializeField]
    private SoundEffect[] _soundEffectList;

    [SerializeField]
    private AudioSource _musicAudioSource;

    [SerializeField]
    private AudioSource _soundEffectAudioSource;

    [SerializeField]
    private AudioSource _customSoundEffectAudioSource;


    private Dictionary<string, int> _notes;
    private const float SEMITONE = 1.05946f;

    private void Start()
    {
        // Declarations

        // Add notes to the dictionary
        _notes = new Dictionary<string, int>
        {
            ["G3"] = -9,
            ["A4"] = -7,
            ["B4"] = -5,
            ["C4"] = -4,
            ["D4"] = -2,
            ["E4"] = 0,
            ["F4"] = 1,
            ["G4"] = 3,
            ["A5"] = 5,
            ["B5"] = 7,
            ["C5"] = 8,
        };

        SetMusicVolume(-10);
        SetSoundEffectVolume(-10);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayCustomSoundEffect("E4");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayCustomSoundEffect("A4");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayCustomSoundEffect("B4");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayCustomSoundEffect("C4");
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
                _soundEffectAudioSource.PlayOneShot(soundEffect.audioClip, soundEffect.volume);

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
                _musicAudioSource.clip = music.audioClip;
                _musicAudioSource.volume = music.volume;
                _musicAudioSource.pitch = music.pitch;
                _musicAudioSource.loop = music.isLooping;

                // Play the audio source
                _musicAudioSource.Play();

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
        _musicAudioSource.Stop();
        _musicAudioSource.clip = null;
    }

    public void SetMusicVolume(float newVolume)
    {
        _musicAudioSource.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", newVolume);
    }

    public void SetSoundEffectVolume(float newVolume)
    {
        _soundEffectAudioSource.outputAudioMixerGroup.audioMixer.SetFloat("SoundEffectVolume", newVolume);
    }

    /**
     * <summary>
     * Changes the volume of the music audio source. (Not the volume of music scriptable object!!!!!
     * So, when the same music is played, the value before volume change will be assigned to the music audio source.)
     * Also MUST be used with StartCoroutine();
     * <paramref name="newVolume"/> is the desired new volume of the music audio source.
     * <paramref name="duration"/> is the period of time for the volume transition. (0 will change it immediately.)
     * </summary>
     */
    private IEnumerator ChangeMusicVolumeCoroutine(float newVolume, float duration)
    {
        // Duration cannot be negative.
        if (duration < 0)
        {
            Debug.Log("Duration must be positive.");
            yield break;
        }

        // Change volume in a specified period of time.
        // If the duration is 0, then change the volume immediately.
        if (duration == 0)
        {
            _musicAudioSource.volume = newVolume;
            yield break;
        }

        // This interval is the amount of volume change every 1 milliseconds.
        var interval = (newVolume - _musicAudioSource.volume) / (duration * 100);
        for (int i = 0; i < ((int)duration * 100); i++)
        {
            _musicAudioSource.volume += interval;
            yield return new WaitForSeconds(0.01f);
        }
    }

    /**
     * <summary>
     * Changes the volume of the music audio source. (Not the volume of music scriptable object!!!!!
     * So, when the same music is played, the value before volume change will be assigned to the music audio source.)
     * <paramref name="newVolume"/> is the desired new volume of the music audio source.
     * <paramref name="duration"/> is the period of time for the volume transition. (0 will change it immediately.)
     * </summary>
     */
    public void ChangeMusicVolume(float newVolume, float duration = 0)
    {
        StartCoroutine(ChangeMusicVolumeCoroutine(newVolume, duration));
    }

    public void PlayCustomSoundEffect(string track)
    {
        // Convert to all upper letters
        track = track.ToUpper();
        
        _customSoundEffectAudioSource.pitch = Mathf.Pow(SEMITONE, _notes[track]);
        _customSoundEffectAudioSource.Play();
    }
}
