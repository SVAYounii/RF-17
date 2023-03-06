using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon_Shooting : MonoBehaviour
{
    [SerializeField] Camera pCamera;
    [SerializeField] float range;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(pCamera.transform.position, pCamera.transform.forward, out hit, range);
        Debug.Log("I hit this thing : " + hit.transform.name);
    }
}
