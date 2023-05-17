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
    //On Start find the required GameObjects/Components and set the CanvasObject's Active bool to false.
    void Start()
    {
        CanvasObject = GameObject.Find("ShardCollectedUI");
        ShardCollectedDescription = GameObject.Find("ShardCollectedUI/ShardCollectedDescription").GetComponent<TextMeshProUGUI>();
        ShardCollectedText = GameObject.Find("ShardCollectedUI/ShardCollectedText").GetComponent<TextMeshProUGUI>();
        CanvasObject.SetActive(false);
    }

    //On trigger set the CanvasObject Active bool to true for 4 seconds then set it to false.
    IEnumerator CanvasDisplayTime()
    {
        CanvasObject.SetActive(true);
        yield return new WaitForSeconds(4);
        CanvasObject.SetActive(false);
    }

    //On trigger set the text of the ShardCollectedText and Description to their respective values from the playerInventory
    //Then start the CanvasDisplayTime Coroutine.
    public void ShowShardUI(playerInventory PI)
    {
        ShardCollectedText.text = PI.Name;
        ShardCollectedDescription.text = PI.Description;
        StartCoroutine(CanvasDisplayTime());
    }
}
