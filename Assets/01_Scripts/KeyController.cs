using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f; // The distance at which the player can interact with the key
    [SerializeField] private KeyCode interactionKey = KeyCode.F; // The key that the player needs to press to pick up the key
    private Transform playerTransform; // Reference to the player's transform

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Check if the player is within interaction distance of the key
        if (Vector3.Distance(transform.position, playerTransform.position) <= interactionDistance)
        {
            Debug.Log("Key is nearby press F!");
            // Check if the player has pressed the interaction key
            if (Input.GetKeyDown(interactionKey))
            {
                Debug.Log("Key pickedup!");
                // Destroy the key object
                Destroy(gameObject);
            }
        }
    }
}
