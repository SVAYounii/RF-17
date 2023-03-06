using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 _playerPos = Vector3.zero;
    private void Update()
    {
            Debug.Log("Moving!");
        if (_playerPos != Vector3.zero)
        {
            float step = 20f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _playerPos, step);

        }
    }

    //private void Ontr(Collision collision)
    //{
    //    if (collision.gameObject.tag != "Player")
    //        return;

    //    /*int money =*/

    //    //collision.gameObject.GetComponent<Enemy>().Hit(55);
    //    Debug.Log("Player Got Hit!");
    //    //if (money > 0)
    //    //    GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<PlayerInfo>().Money += money;
    //    Destroy(this.gameObject);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        /*int money =*/

        //collision.gameObject.GetComponent<Enemy>().Hit(55);
        Debug.Log("Player Got Hit!");
        //if (money > 0)
        //    GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<PlayerInfo>().Money += money;
        Destroy(this.gameObject);
    }

    public void Move(Vector3 playerPos)
    {
        _playerPos= playerPos;
    }
}
