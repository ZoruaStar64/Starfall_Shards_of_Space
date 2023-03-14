using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePlayerToSpawn : MonoBehaviour
{
    public GameObject playerChar;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        PutPlayerOnSpawnpoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutPlayerOnSpawnpoint()
    {
        playerChar.transform.position = transform.position + offset;
    }
}
