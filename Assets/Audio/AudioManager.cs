using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicTrack
{
    Menu,
    Game
}

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private MusicTrack startingTrack;

    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource menuSource;

    [SerializeField]
    private List<AudioClip> musicClips;

    public static AudioManager instance;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

        if (!instance.CheckIsTrackPlaying(startingTrack))
            instance.PlayMusic(startingTrack);

    }

    /// Functions ///
    
    private bool CheckIsTrackPlaying(MusicTrack track)
    {
        return musicSource.clip == musicClips[(int)track];
    }

    public void PlayMusic(MusicTrack track)
    {
        if (musicSource.isPlaying)
            musicSource.Stop();

        musicSource.clip = musicClips[(int)track];
        musicSource.Play();
    }


    public void PlayMenuClip()
    {
        if (menuSource.isPlaying)
            menuSource.Stop();

        menuSource.Play();
    }

}
