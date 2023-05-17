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
    //if the player holds the left mouse button release the mouse from it's confines (let the player control their cursor)
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

    //Calculate the camera rotation depending on the player's mouse movement
    void calculateCameraRotation()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        transform.position = playerChar.transform.position;
    }

    //get the player's playerMovement script component and check if the hasWallJumped function is true
    //this will probably be repurposed for a 2d walljumping check maybe?
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
