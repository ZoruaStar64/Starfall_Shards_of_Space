using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointManager : MonoBehaviour
{

    public static SpawnpointManager Instance;
    public GameObject CurrentSpawn;
    public GameObject playerChar;
    public Vector3 offset;

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
    void Start()
    {
        CurrentSpawn = MainManager.Instance.CurrentSpawn;
        PutPlayerOnSpawnpoint();
    }

    public void PutPlayerOnSpawnpoint()
    {
        CurrentSpawn = MainManager.Instance.CurrentSpawn;
        Vector3 SpawnPos = CurrentSpawn.transform.position;
        playerChar.transform.position = SpawnPos + offset;
        //playerChar.transform.position = new Vector3(SpawnPos.x, playerChar.transform.position.y, SpawnPos.y);
        print(CurrentSpawn.transform.position + "");
    }
}
