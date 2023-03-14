using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardFunctions : MonoBehaviour
{
    public Material CollectedShardShield;
    public Material CollectedShardMaterial;
    public bool ShardCollected;
    public GameObject SpawnPoint;
    public GameObject Shard;
    public string Name;
    public string Description;
    AudioSource CollectSound;

    void Start()
    {
        CollectSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInventory PI = other.GetComponent<playerInventory>();
        //TakePlayerToSpawn SP = SpawnPoint.GetComponent<TakePlayerToSpawn>();

        if (PI != null && ShardCollected == true)
        {
            print("Shard is already collected. Play animation and award some Space Tokens.");
            //SP.PutPlayerOnSpawnpoint();
        }

        if (PI != null && ShardCollected == false)
        {
            PI.shardCollected(Name, Description);
            gameObject.GetComponent<MeshRenderer>().material = CollectedShardShield;
            Shard.GetComponent<MeshRenderer>().material = CollectedShardMaterial;
            //SP.PutPlayerOnSpawnpoint();
            ShardCollected = true;
            CollectSound.Play();
        }
    }
}
