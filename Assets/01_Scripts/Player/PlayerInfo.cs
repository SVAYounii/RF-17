using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] int playerHealth;
    public enum State
    {
        Alive,
        Dead
    }
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage) {
        if (state == State.Dead)
        {
            return;
        }
        playerHealth -= damage;
        if(playerHealth<=0) 
        {
            state = State.Dead;
            return;
        }
    }
    public void HealUp(int heal)
    {
        if (state == State.Alive)
        {
            if(playerHealth + heal > 100) 
            {
                playerHealth = 100;
            }
            else
            {
                playerHealth += heal;
            }
            return;
        }
    }
}
