using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{

    public int MusicId;
    public GameObject BackgroundMusic;
    
    //Looks for the BackgroundMusic GameObject
    void Start()
    {
        BackgroundMusic = GameObject.Find("BackgroundMusic");
    }

    //When entering the collider switch the MainManager's MusicId value to the Int on this script's Object.
    //Then activate the BackgroundMusic GameObject's SwitchMusic function.
    private void OnTriggerEnter(Collider other)
    {
        MainManager.Instance.MusicId = MusicId;
        BackgroundMusic.GetComponent<BackgroundMusic>().SwitchMusic();
    }
}
