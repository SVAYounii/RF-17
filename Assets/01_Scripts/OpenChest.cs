using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Press F to open the chest!");     
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerWeapon weapon = other.GetComponentInChildren<PlayerWeapon>();
            MaxAmmo(weapon);
        }
    }


    private void MaxAmmo(PlayerWeapon weapon)
    {
        weapon.resetstoredammo();
    }    
   
}

