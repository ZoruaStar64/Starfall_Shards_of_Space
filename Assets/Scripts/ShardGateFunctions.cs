using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardGateFunctions : MonoBehaviour
{
    public int RequiredShards;
    public int shardCount { get; private set; }

    //On trigger get the player's playerInventory component and check if PI isn't null and if their shardCount equals or is higher
    //than this gate's RequiredShards Variable if it is set this gameObject's Active bool to false else print that they dont have enough shards.
    private void OnTriggerEnter(Collider other)
    {
        playerInventory PI = other.GetComponent<playerInventory>();

        if (PI != null && PI.shardCount >= RequiredShards)
        {
            print("Player has enough shards, open the gate");
            gameObject.SetActive(false);
        }

        if (PI != null && PI.shardCount < RequiredShards)
        {
            print("not enough shards");
        }
    }
}
