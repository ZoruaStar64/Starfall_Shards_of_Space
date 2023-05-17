using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject RotationPoint;
    public Transform rotatePoint;
    public float speed = 100;
    public float rotationValue;

    // Start is called before the first frame update
    //set this object's transform's rotation to the base values
    void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    //rotate this object's y axis by 1 * speed * Time.deltaTime
    void Update()
    {
        transform.Rotate(0, (1 * speed * Time.deltaTime), 0);
    }
}
