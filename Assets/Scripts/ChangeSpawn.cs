using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpawn : MonoBehaviour
{

    public GameObject NewSpawn;
    public GameObject SpawnPointManager;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPointManager = GameObject.Find("SpawnPointManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (NewSpawn != MainManager.Instance.CurrentSpawn)
        {
            MainManager.Instance.CurrentSpawn = NewSpawn;
            //SpawnPointManager.GetComponent<SpawnpointManager>().PutPlayerOnSpawnpoint();
        }
    }
}
