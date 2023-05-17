using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public static BackgroundMusic Instance;

    public AudioClip[] MusicClips;
    public AudioSource Audio;
    public int currentSong = 0;
    public bool sameClip = false;

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

    //On Start trigger the ActivateMusic function.
    void Start()
    {
        ActivateMusic();
    }

    //When the this function gets triggered change the currenSong variable to that of MainManager's MusicId variable
    //Then trigger the ActivateMusic function.
    public void SwitchMusic()
    {
        currentSong = MainManager.Instance.MusicId;
        ActivateMusic();
    }

    //upon triggering this function change the source variable to this object's AudioSource component
    //Then switch sameClip to false. After this go through a switch and check if currentSong equals to a number
    //if a case is met check if the source.clip is the same as MusicClips. if not then make source.clip that case's MusicClips
    //If it is the same then set sameClip to true and break the switch
    //if source.clip is equal to Audio.clip and sameClip is equal to false then stop the previous clip and start the new one.
    public void ActivateMusic()
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        sameClip = false;

        switch (currentSong)
        {
            case 0:
                if (source.clip != MusicClips[0])
                {
                    source.clip = MusicClips[0];
                    break;
                }
                sameClip = true;
                break;
            case 1:
                if (source.clip != MusicClips[1])
                {
                    source.clip = MusicClips[1];
                    break;
                }
                sameClip = true;
                break;
            case 2:
                if (source.clip != MusicClips[2])
                {
                    source.clip = MusicClips[2];
                    break;
                }
                sameClip = true;
                break;
            case 3:
                if (source.clip != MusicClips[3])
                {
                    source.clip = MusicClips[3];
                    break;
                }
                sameClip = true;
                break;
            case 4:
                if (source.clip != MusicClips[4])
                {
                    source.clip = MusicClips[4];
                    break;
                }
                sameClip = true;
                break;
            case 5:
                if (source.clip != MusicClips[5])
                {
                    source.clip = MusicClips[5];
                    break;
                }
                sameClip = true;
                break;
            case 6:
                if (source.clip != MusicClips[6])
                {
                    source.clip = MusicClips[6];
                    break;
                }
                sameClip = true;
                break;
            default:
                source.clip = MusicClips[0];
                break;
        }
        if (source.clip == Audio.clip && sameClip == false)
        {
            Audio.Stop();
            Audio.clip = source.clip;
            Audio.volume = 0.5f;
            Audio.Play();
        }
    }
}
