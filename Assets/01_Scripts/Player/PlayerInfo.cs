using System.Collections.Generic;
using TMPro;
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
    public List<DoorKeyCard> doorKeyCards = new List<DoorKeyCard>();

    [SerializeField] TextMeshProUGUI RedCardText;
    [SerializeField] TextMeshProUGUI YellowCardText;
    [SerializeField] TextMeshProUGUI BlueCardText;
    [SerializeField] TextMeshProUGUI InteractKeyText;

    private bool isPickUpActive = false;
    public bool IsPickupActive
    {
        get => this.isPickUpActive;
        set
        {
            this.isPickUpActive = value;
            this.RefreshCardGUI();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.RefreshCardGUI();
    }

    public void TakeDamage(int damage)
    {
        if (state == State.Dead)
        {
            return;
        }
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            state = State.Dead;
            return;
        }
    }

    public void HealUp(int heal)
    {
        if (state == State.Alive)
        {
            if (playerHealth + heal > 100)
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

    public bool HasKeyCard(DoorKeyCard card)
    {
        if (card.Equals(DoorKeyCard.None))
        {
            return true;
        }

        var result = this.doorKeyCards.Contains(card);
        if (result)
        {
            this.doorKeyCards.Remove(card);
            this.RefreshCardGUI();
        }

        return result;
    }

    public bool AddKeyCard(DoorKeyCard card)
    {
        if (card.Equals(DoorKeyCard.None))
        {
            return true;
        }

        this.doorKeyCards.Add(card);
        this.isPickUpActive = false;
        this.RefreshCardGUI();
        return true;
    }

    public void RefreshCardGUI()
    {
        int red = 0;
        int yellow = 0;
        int blue = 0;

        foreach (var card in this.doorKeyCards)
        {
            if (card.Equals(DoorKeyCard.Red)) 
                red++;
            if (card.Equals(DoorKeyCard.Yellow))
                yellow++;
            if (card.Equals(DoorKeyCard.Blue))
                blue++;
        }

        if (this.RedCardText != null)
            this.RedCardText.text = red.ToString();
        if (this.YellowCardText != null)
            this.YellowCardText.text = yellow.ToString();
        if (this.BlueCardText != null)
            this.BlueCardText.text = blue.ToString();

        if (this.InteractKeyText != null)
            this.InteractKeyText.text = this.IsPickupActive ? "Press [F]" : "";
    }
}
