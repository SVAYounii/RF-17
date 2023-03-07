using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Camera pCamera;
    [SerializeField] float range=100f;
    [SerializeField] int damage=10;
    [SerializeField] int maxAmmo = 30;
    [SerializeField] int currentAmmo;
    [SerializeField] int storedAmmo;
    private float timePassed;
    [SerializeField] float fireRate=0.2f;
    private void Start()
    {
        currentAmmo = maxAmmo;
        pCamera = Camera.main;
    }
    void Update()
    {
        timePassed += Time.deltaTime;
        if (Input.GetButton("Fire1")&&timePassed>fireRate)
        {
            Shoot();
            timePassed= 0;
        }
    }
    private void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo -= 1;
            RaycastHit hit;
            if (Physics.Raycast(pCamera.transform.position, pCamera.transform.forward, out hit, range))
            {
                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target == null)
                    return;
                target.Hit(damage);
                Debug.Log("I hit this thing : " + hit.transform.name);
            }
            else
            {
                return;
            }
        }
        else
        {
            Reload();
        }
    }
    private void Reload()
    {
        int missingAmmo = maxAmmo - currentAmmo;
        if (storedAmmo - missingAmmo <= 0)
        {
            currentAmmo += storedAmmo;
            storedAmmo = 0;
        }
        else
        {
            currentAmmo += missingAmmo;
            storedAmmo -= missingAmmo;
        }
    }
}
