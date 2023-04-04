using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToTutorialForest : MonoBehaviour
{
    public string EntryDirection = "Entrance";
    public GameObject NewSpawn;
    public GameObject SpawnPointManager;

    private void Start()
    {
        SpawnPointManager = GameObject.Find("SpawnPointManager");
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TransportToTutorial());
    }
    public IEnumerator TransportToTutorial()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("TutorialForest");

        if (EntryDirection == "Entrance")
        {
            NewSpawn = GameObject.Find("Spawnpoint_3");
        }
        if (EntryDirection == "Exit")
        {
            NewSpawn = GameObject.Find("Spawnpoint_4");
        }
        MainManager.Instance.CurrentSpawn = NewSpawn;
        SpawnPointManager.GetComponent<SpawnpointManager>().PutPlayerOnSpawnpoint();
    }
}
