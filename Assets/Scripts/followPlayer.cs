using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
     public GameObject playerChar;
     public Vector3 offset;
     // Start is called before the first frame update
     void Start()
     {
         //offset = new Vector3(4,4,4);
         //offsetX = new Vector3(0, 4, 4);
         //offsetY = new Vector3(0, 0, 4);
     }

     // Update is called once per frame
     void Update()
     {
        //transform.position = playerChar.transform.position + offset;
        //transform.LookAt(playerChar.transform);
     }
}
