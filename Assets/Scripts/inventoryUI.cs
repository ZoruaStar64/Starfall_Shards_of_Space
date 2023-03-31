using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inventoryUI : MonoBehaviour
{
    private TextMeshProUGUI coinTotal;
    private TextMeshProUGUI shardTotal;

    // Start is called before the first frame update
    void Start()
    {
        coinTotal = GameObject.Find("CoinCounter/CoinTotal").GetComponent<TextMeshProUGUI>();
        shardTotal = GameObject.Find("ShardCounter/ShardTotal").GetComponent<TextMeshProUGUI>();
        coinTotal.text = MainManager.Instance.Coins.ToString();
        shardTotal.text = MainManager.Instance.Shards.ToString();
    }

    public void UpdateCoinTotal(playerInventory PI)
    {
        coinTotal.text = PI.coinCount.ToString();
    }

    public void UpdateShardTotal(playerInventory PI)
    {
        shardTotal.text = PI.shardCount.ToString();
    }
}
