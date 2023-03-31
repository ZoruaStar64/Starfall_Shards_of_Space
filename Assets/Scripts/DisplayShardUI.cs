using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayShardUI : MonoBehaviour
{
    private GameObject CanvasObject;
    private TextMeshProUGUI ShardCollectedText;
    private TextMeshProUGUI ShardCollectedDescription;
    // Start is called before the first frame update
    void Start()
    {
        CanvasObject = GameObject.Find("ShardCollectedUI");
        ShardCollectedDescription = GameObject.Find("ShardCollectedUI/ShardCollectedDescription").GetComponent<TextMeshProUGUI>();
        ShardCollectedText = GameObject.Find("ShardCollectedUI/ShardCollectedText").GetComponent<TextMeshProUGUI>();
        CanvasObject.SetActive(false);
    }

    IEnumerator CanvasDisplayTime()
    {
        CanvasObject.SetActive(true);
        yield return new WaitForSeconds(4);
        CanvasObject.SetActive(false);
    }

    public void ShowShardUI(playerInventory PI)
    {
        ShardCollectedText.text = PI.Name;
        ShardCollectedDescription.text = PI.Description;
        StartCoroutine(CanvasDisplayTime());
    }
}
