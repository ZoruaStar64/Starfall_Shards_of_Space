using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointManager : MonoBehaviour
{

    public static SpawnpointManager Instance;
    public GameObject CurrentSpawn;
    public GameObject playerChar;
    public Vector3 offset;

    //set Singleton instance
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    //set the CurrentSpawn to MainManager's CurrentSpawn variable
    //Afterwards trigger the PutPlayerOnSpawnPoint function.
    void Start()
    {
        CurrentSpawn = MainManager.Instance.CurrentSpawn;
        PutPlayerOnSpawnpoint();
    }

    //set the CurrentSpawn to MainManager's CurrentSpawn variable
    //set the SpawnPos variable to the CurrentSpawn's position and then put the playerChar on the SpawnPos + offset
    public void PutPlayerOnSpawnpoint()
    {
        CurrentSpawn = MainManager.Instance.CurrentSpawn;
        Vector3 SpawnPos = CurrentSpawn.transform.position;
        playerChar.transform.position = SpawnPos + offset;
        //playerChar.transform.position = new Vector3(SpawnPos.x, playerChar.transform.position.y, SpawnPos.y);
        print(CurrentSpawn.transform.position + "");
    }
}
