using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    GameObject Player;
    [SerializeField] NavMeshAgent agent;

    public float Health;
    private float _baseHealth = 100;
    public int Damage = 15;
    public int FindingRange = 20;

    public float ShootDelay = 2;
    float _nextTime = 0;
    int _callOnce = 0;

    public GameObject DeathParticle;
    public GameObject Bullet;
    public Transform Barrel;

    public enum State
    {
        None,
        Attacking,
        Dead
    };
    public State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.None;
        Player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
        Health = _baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.None:
                //Wait until player is close
                IsPlayerClose();
                break;
            case State.Attacking:
                //if player has lost the enemy after an attack
                IsPlayerLost();
                break;
        }
    }

    void IsPlayerClose()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }
        if (distance <= FindingRange)
        {
            //if player is close to enemy
            state = State.Attacking;
        }
    }

    void ShootAtPlayer(Vector3 playerPos)
    {
        Debug.Log("Shoot!");
        GameObject bullet = Instantiate(Bullet, Barrel.position, new Quaternion());
        bullet.GetComponent<Bullet>().Move(playerPos);
    }

    void IsPlayerLost()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        //Enemy is too close to the player so stop
        //transform.rotation = Quaternion.LookRotation(Player.transform.position);
        if (distance <= 10)
        {
            agent.isStopped = true;
            Vector3 _direction = (Player.transform.position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 2f);
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(Player.transform.position);

        }

        if (distance <= FindingRange - 5)
        {
            Vector3 playerPos = Vector3.zero;

            //start to shoot
            if (_callOnce == 0)
            {
                _callOnce = 1;
                _nextTime = Time.time + ShootDelay;
            }
            else if (Time.time > _nextTime - 1f && _callOnce == 1)
            {
                playerPos = Player.transform.position;
                if (Time.time > _nextTime && _callOnce == 1)
                {
                    _callOnce = 0;
                    ShootAtPlayer(playerPos);

                }
            }


        }

        if (distance >= FindingRange + 5)
        {
            //if player has lost the enemy after an attack
            state = State.None;
        }
    }

    public void Hit(int amountDamage)
    {
        if (state == State.Dead)
            return;

        Health -= amountDamage;
        if (Health <= 0)
        {
            state = State.Dead;
            GameObject p = Instantiate(DeathParticle, transform.position, new Quaternion(-90, 0, 0, 0));
            //Transform t = GameObject.FindGameObjectsWithTag("ParticleParent").FirstOrDefault().transform;
            //p.transform.parent = t;
            Destroy(this.gameObject);

            //return Random.Range(40, 60);
            return;

        }

        float percent = Health / _baseHealth;
        //return 0;
    }
}
