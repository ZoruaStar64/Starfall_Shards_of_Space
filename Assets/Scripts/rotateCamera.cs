using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCamera : MonoBehaviour
{
    public GameObject playerChar;
    public float sensitivity = 2f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        calculateCameraRotation();
        if (Input.GetMouseButton(1))
        { 
            Cursor.lockState = CursorLockMode.None;
        }
        if (!Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void calculateCameraRotation()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        transform.position = playerChar.transform.position;
    }

    public bool hasWallJumped()
    {
        playerMovement PM = playerChar.GetComponent<playerMovement>();
        if(PM.hasWallJumped == true)
        {
            return true;
        }
        return false;
    }
}
