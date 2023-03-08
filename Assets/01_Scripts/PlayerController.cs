using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float distance = 2f;

    // Update is called once per frame
    void Update()
    {
        // Move the player cube
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(x, 0, z);

        // Check for nearby cubes to open
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            if (hit.collider.CompareTag("Chest"))
            {
                Debug.Log("Press F to open the chest!");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("You opened this chest!");
                }
            }
        }
    }
}