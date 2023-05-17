using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inventoryUI : MonoBehaviour
{
    private TextMeshProUGUI coinTotal;
    private TextMeshProUGUI shardTotal;

    // Start is called before the first frame update
    //Find and the coin/shardtotal GameObjects and get their TextMeshProUGUI components
    //also set the coin and shardtotal texts to their MainManager values
    //the MainManager values use ToString so they can get converted properly.
    void Start()
    {
        coinTotal = GameObject.Find("CoinCounter/CoinTotal").GetComponent<TextMeshProUGUI>();
        shardTotal = GameObject.Find("ShardCounter/ShardTotal").GetComponent<TextMeshProUGUI>();
        coinTotal.text = MainManager.Instance.Coins.ToString();
        shardTotal.text = MainManager.Instance.Shards.ToString();
    }

    //when triggered make the coinTotal's text to playerInventory's coinCount
    public void UpdateCoinTotal(playerInventory PI)
    {
        coinTotal.text = PI.coinCount.ToString();
    }

    //when triggered make the shardTotal's text to playerInventory's shardCount
    public void UpdateShardTotal(playerInventory PI)
    {
        shardTotal.text = PI.shardCount.ToString();
    }
}
