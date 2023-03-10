using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private GameObject playerRef = null;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (playerRef != null)
        {
            Debug.Log("Press F to open the chest!");
            if (Input.GetKeyDown(KeyCode.F))
            {
                MaxAmmo();

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            this.playerRef = other.gameObject;
        }
    }


    private void MaxAmmo()
    {

        PlayerWeapon weapon = playerRef.GetComponentInChildren<PlayerWeapon>();
        weapon.resetstoredammo();
    }    

   
}

