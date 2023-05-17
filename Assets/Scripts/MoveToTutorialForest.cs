using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToTutorialForest : MonoBehaviour
{
    public string EntryDirection = "Entrance";
    //public GameObject NewSpawn;
    public GameObject PlayerChar;
    //public GameObject SpawnPointManager;

    //On start find the Player character
    private void Start()
    {
        //SpawnPointManager = GameObject.Find("SpawnPointManager");
        PlayerChar = GameObject.Find("Player");
    }

    //On trigger start the TransportToTutorial Coroutine
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TransportToTutorial());
    }

    //Wait for 1 second then load the TutorialForestScene afterwards depending on the EntryDirection put the Player on the correct spot.
    public IEnumerator TransportToTutorial()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("TutorialForest");

        if (EntryDirection == "Entrance")
        {
            PlayerChar.transform.position = new Vector3(0, 1, 10);
            //NewSpawn = GameObject.Find("Spawnpoint_3");
        }
        if (EntryDirection == "Exit")
        {
            PlayerChar.transform.position = new Vector3(0, 1, 140);
            //NewSpawn = GameObject.Find("Spawnpoint_4");
        }
        //MainManager.Instance.CurrentSpawn = NewSpawn;
        //SpawnPointManager.GetComponent<SpawnpointManager>().PutPlayerOnSpawnpoint();
    }
}
