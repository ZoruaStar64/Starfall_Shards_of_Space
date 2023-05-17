using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidlingFunctions : MonoBehaviour
{
    //Currently has no function other than existing
    private void OnTriggerEnter(Collider other)
    {
        playerInventory PI = other.GetComponent<playerInventory>();

        if (PI != null)
        {
            PI.coinCollected();
            gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
