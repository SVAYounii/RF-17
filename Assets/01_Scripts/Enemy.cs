using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    GameObject Player;
    [SerializeField] NavMeshAgent agent;

    public float Health;
    private float baseHealth = 100;
    public int Damage = 15;

    public GameObject DeathParticle;

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
        Health = baseHealth;
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
        Debug.Log(Vector3.Distance(transform.position, Player.gameObject.transform.position));
        //Enemy is too close to the player so stop
        if (Vector3.Distance(transform.position, Player.gameObject.transform.position) <= 5)
        {
            agent.isStopped = true;
            return;
        }

        if (Vector3.Distance(transform.position, Player.gameObject.transform.position) <= 20)
        {
            //if player is close to enemy
            agent.SetDestination(Player.gameObject.transform.position);
            state = State.Attacking;
        }
    }

    void IsPlayerLost()
    {
        if (Vector3.Distance(transform.position, Player.gameObject.transform.position) <= 15)
        {
            //if player has lost the enemy after an attack
            state = State.None;
        }
    }

    public int Hit(int amountDamage)
    {
        if (state == State.Dead)
            return 0;

        Health -= amountDamage;
        if (Health <= 0)
        {
            state = State.Dead;
            GameObject p = Instantiate(DeathParticle, transform.position, new Quaternion(-90, 0, 0, 0));
            Transform t = GameObject.FindGameObjectsWithTag("ParticleParent").FirstOrDefault().transform;
            p.transform.parent = t;
            Destroy(this.gameObject);

            return Random.Range(40, 60);
        }

        float percent = Health / baseHealth;
        return 0;
    }
}
