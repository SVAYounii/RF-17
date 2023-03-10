using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 _playerPos = Vector3.zero;
    float _timeToDestroy = 1f;
    float _nextTime;
    bool callOnce;
    private void Update()
    {
        Debug.Log("Moving!");
        if (_playerPos != Vector3.zero)
        {
            float step = 20f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _playerPos, step);
            if (!callOnce)
            {
                callOnce = true;
                _nextTime = Time.time + _timeToDestroy;
            }
            if (Time.time > _nextTime)
            {
                Destroy(this.gameObject);
            }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Debug.Log("object!");

            Destroy(this.gameObject);
            return;
        }

        /*int money =*/

        collision.gameObject.GetComponent<PlayerInfo>().TakeDamage(10);
        Debug.Log("Player Got Hit!");
        //if (money > 0)
        //    GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<PlayerInfo>().Money += money;
        Destroy(this.gameObject);
    }

    public void Move(Vector3 playerPos)
    {
        _playerPos = playerPos;
    }
}
