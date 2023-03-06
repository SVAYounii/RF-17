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
        if (Physics.Raycast(pCamera.transform.position, pCamera.transform.forward, out hit, range))
        {
            /*Uncomment this line when you are setting up the enemy healthbars so that the shooting
             * script can call the healthbar reduce method*/
            //EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            Debug.Log("I hit this thing : " + hit.transform.name);
        }
        else
        {
            return;
        }
    }
}
