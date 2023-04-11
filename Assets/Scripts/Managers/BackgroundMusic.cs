using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public static BackgroundMusic Instance;

    public AudioClip[] MusicClips;
    public AudioSource Audio;
    public int currentSong = 0;

    // Singelton to keep instance alive through all scenes
    void Awake()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {
        currentSong = MainManager.Instance.MusicId;
        SwitchMusic();
    }

    public void SwitchMusic()
    {
        currentSong = MainManager.Instance.MusicId;
        AudioSource source = gameObject.GetComponent<AudioSource>();

        switch (currentSong)
        {
            case 0:
                source.clip = MusicClips[0];
                break;
            case 1:
                source.clip = MusicClips[1];
                break;
            case 2:
                source.clip = MusicClips[2];
                break;
            case 3:
                source.clip = MusicClips[3];
                break;
            default:
                source.clip = MusicClips[2];
                break;

                
        }
        if (source.clip == Audio.clip)
        {
            Audio.Stop();
            Audio.clip = source.clip;
            Audio.Play();
        }
    }
}
