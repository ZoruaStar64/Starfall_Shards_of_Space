using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinFunctions : MonoBehaviour
{

    AudioSource CollectSound;

    //Get this Object's AudioSource Component.
    void Start()
    {
        CollectSound = GetComponent<AudioSource>();
    }

    //upon trigger play the CollectSound Audio then wait for 0.3 seconds then finally set this gameObject's Active bool to false.
    IEnumerator PlayCollectionSound()
    {
        CollectSound.Play();
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }

    //On trigger get the player's playerInventory script component
    //if playerInventory is not null then trigger PI's coinCollected function and start the PlayColectionSound Coroutine.
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
