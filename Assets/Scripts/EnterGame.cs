using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When the game is started by pressing the Star Button on the title screen load the Hub Scene.
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("HubOutskirts");
    }
}
