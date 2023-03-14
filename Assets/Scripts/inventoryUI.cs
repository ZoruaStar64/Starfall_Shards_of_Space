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
        coinTotal = GetComponent<TextMeshProUGUI>();
        shardTotal = GetComponent<TextMeshProUGUI>();
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
