using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinFunctions : MonoBehaviour
{

    AudioSource CollectSound;

    void Start()
    {
        CollectSound = GetComponent<AudioSource>();
    }

    IEnumerator PlayCollectionSound()
    {
        CollectSound.Play();
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInventory PI = other.GetComponent<playerInventory>();

        if (PI != null) 
        {
            PI.coinCollected();
            StartCoroutine("PlayCollectionSound");
        }
    }
}
