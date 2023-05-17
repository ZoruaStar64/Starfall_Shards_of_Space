using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerInventory : MonoBehaviour
{

    public int coinCount { get; private set; }
    public int shardCount { get; private set; }
    public string Name = "Placeholder Title";
    public string Description = "Placeholder Description";

    public UnityEvent<playerInventory> OnCoinCollected;
    public UnityEvent<playerInventory> OnShardCollected;

    //Check if the MainManager instance is not null if so then coinCount and ShardCount become the values in MainManager.
    private void Start()
    {
        if(MainManager.Instance != null)
        {
            coinCount = MainManager.Instance.Coins;
            shardCount = MainManager.Instance.Shards;
        }
    }

    //Upon collecting a coin Update the coinCount variable by 1 then invoke the OnCoinCollected function.
    //Also make MainManager's Coins variable equal to coinCoint.
    public void coinCollected()
    {
        coinCount++;
        OnCoinCollected.Invoke(this);
        MainManager.Instance.Coins = coinCount;
    }

    //Upon collecting a Shard update the shardCount by 1. If the ShardName is not null then the Name variable becomes ShardName
    //if ShardDescription is not null then the Description variable becomes ShardDescription
    //Then Invoke the OnShardCollected function and make MainManager's Shards variable equal to shardCount.
    public void shardCollected(string ShardName, string ShardDescription)
    {
        shardCount++;
        if (ShardName != null)
        {
            Name = ShardName;
        }
        if (ShardDescription != null)
        {
            Description = ShardDescription;
        }
        OnShardCollected.Invoke(this);
        MainManager.Instance.Shards = shardCount;
    }
}
