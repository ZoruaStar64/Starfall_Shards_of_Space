using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    RectTransform rectTransform;

    // Start is called before the first frame update
    //get this object's RectTransform Component
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    //Rotate this Object's z axis by 0.1f
    void Update()
    {
        rectTransform.Rotate(0, 0, 0.1f);
    }
}
