using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy")
            return;

        int money = collision.gameObject.GetComponent<Enemy>().Hit(55);
        //if (money > 0)
        //    GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<PlayerInfo>().Money += money;
        Destroy(this.gameObject);
    }
}
