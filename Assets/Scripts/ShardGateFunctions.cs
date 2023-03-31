using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardGateFunctions : MonoBehaviour
{
    public int RequiredShards;
    public int shardCount { get; private set; }

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
