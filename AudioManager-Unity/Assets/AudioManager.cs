/*
 * This is an all purpose Audio Manager that can play music and sound effect separately.
 * 
 * TO-DO LIST:
 * - Play music and sound effect using one audio source (DONE)
 * - Keep playing the music where it left off across scenes (NOT DONE)
 * - Produce very basic sound effects using code (NOT DONE)
 * - Use an audio mixer to control the volume of all musics in the game (DONE)
 * - Change the volume of a music in the runtime over a period of time or immediately (NOT DONE)
 */

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField]
    private Music[] _musicList;

    [SerializeField]
    private SoundEffect[] _soundEffectList;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        SetVolume(-10);

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

    public void SetVolume(float newVolume)
    {
        _audioSource.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", newVolume);
    }

    public void ChangeMusicVolume(string audioName, float newVolume)
    {
        // Change volume in a specified period of time.
    }
}
