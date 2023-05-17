using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpawn : MonoBehaviour
{

    public GameObject NewSpawn;
    public GameObject SpawnPointManager;
    // Start is called before the first frame update
    //Looks for the SpawnPointManager GameObject.
    void Start()
    {
        SpawnPointManager = GameObject.Find("SpawnPointManager");
    }

    //If the NewSpawn GameObject Variable is not equal to the MainManager's CurrenSpawn variable.
    //Change the MainManager variable to that of the NewSpawn GameObject.
    private void OnTriggerEnter(Collider other)
    {
        if (NewSpawn != MainManager.Instance.CurrentSpawn)
        {
            MainManager.Instance.CurrentSpawn = NewSpawn;
            //SpawnPointManager.GetComponent<SpawnpointManager>().PutPlayerOnSpawnpoint();
        }
    }
}
