using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialToHub : MonoBehaviour
{

    public string ExitDirection = "Exit";
    public GameObject PlayerChar;

    // Start is called before the first frame update
    //Finds the player character.
    void Start()
    {
        PlayerChar = GameObject.Find("Player");
    }

    //On trigger start the TransportToHub Coroutine.
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TransportToHub());
    }

    //Wait for 1 second then load the HubOutskirts scene.
    //Depending on from which direction the player exits put them at the correct position.
    public IEnumerator TransportToHub()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("HubOutskirts");

        if (ExitDirection == "Entrance")
        {
            PlayerChar.transform.position = new Vector3(30, 1, 33);
        }
        if (ExitDirection == "Exit")
        {
            PlayerChar.transform.position = new Vector3(30, 1, 72);
        }
    }
}
