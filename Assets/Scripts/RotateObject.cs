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
    void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, (1 * speed * Time.deltaTime), 0);
    }
}
