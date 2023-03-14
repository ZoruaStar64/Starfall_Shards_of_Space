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

    public void coinCollected()
    {
        coinCount++;
        OnCoinCollected.Invoke(this);
    }

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
    }
}
