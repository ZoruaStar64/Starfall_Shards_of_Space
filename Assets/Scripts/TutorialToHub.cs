using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialToHub : MonoBehaviour
{

    public string ExitDirection = "Exit";
    public GameObject PlayerChar;

    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TransportToHub());
    }
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
