using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sounds[] musicSounds;
    public AudioSource musicSource;

    Button button;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sounds s = Array.Find(musicSounds, s => s.name == name);

        if(s == null)
        {
            //Debug.Log("Sound not found.");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;

        
        button = GetComponent<Button>();
        button.interactable = true;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
