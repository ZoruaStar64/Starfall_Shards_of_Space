using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePlayerToSpawn : MonoBehaviour
{
    public GameObject playerChar;
    public Vector3 offset;

    // Start is called before the first frame update
    //Trigger the PutPlayerOnSpawnpoint function
    void Start()
    {
        PutPlayerOnSpawnpoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set the player's position to this spawnpoint's position + offset
    public void PutPlayerOnSpawnpoint()
    {
        playerChar.transform.position = transform.position + offset;
    }
}
