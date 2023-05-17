using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientAnimation : MonoBehaviour
{
    public float speed;
    public Color startColor;
    public Color endColor;
    float startTime;
    // Start is called before the first frame update
    //Sets the startTime to Time.deltaTime.
    void Start()
    {
        startTime = Time.deltaTime;
    }

    // Update is called once per frame
    //sets the t float to the math equation and set this object's Renderer component to the Color.Lerp
    void Update()
    {
        float t = (Mathf.Sin(Time.time - startTime) * speed);
        GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
    }
}
