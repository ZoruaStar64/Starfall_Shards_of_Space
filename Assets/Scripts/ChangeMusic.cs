using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{

    public int MusicId;
    public GameObject BackgroundMusic;
    
    void Start()
    {
        BackgroundMusic = GameObject.Find("BackgroundMusic");
    }

    private void OnTriggerEnter(Collider other)
    {
        MainManager.Instance.MusicId = MusicId;
        BackgroundMusic.GetComponent<BackgroundMusic>().SwitchMusic();
    }
}
