using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField]
    private float sensX;
    [SerializeField]
    private float sensY;
    [SerializeField]
    private Transform look_direction;

    //Up down
    private float xRotation;
    //Left right
    private float yRotiation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotiation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotate the camera on the two axis
        transform.rotation = Quaternion.Euler(xRotation, yRotiation, 0);

        //Rotate player to left and right (No need to rotate it up and down)
        look_direction.rotation = Quaternion.Euler(0, yRotiation, 0);
    }
}
