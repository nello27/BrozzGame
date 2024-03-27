using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    private bool isMusicPlaying = false;

    void Awake()
    {
        // Singleton pattern to ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Reproduce la música de fondo cuando se inicia el juego
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (!isMusicPlaying)
        {
            // Configura el clip de música de fondo y reproduce la música en bucle
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
            isMusicPlaying = true;
        }
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
        isMusicPlaying = false;
    }

    public void PauseBackgroundMusic()
    {
        musicSource.Pause();
        isMusicPlaying = false;
    }

    public void ResumeBackgroundMusic()
    {
        musicSource.UnPause();
        isMusicPlaying = true;
    }

    public void SetMusicVolume(float volume)
    {
        // Ajusta el volumen de la música de fondo
        musicSource.volume = volume;
    }
}
