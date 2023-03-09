using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    public DoorKeyCard keyCard = DoorKeyCard.None;
    private bool isPickUpActive = false;
    private GameObject playerRef = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPickUpActive && playerRef != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                this.isPickUpActive = false;
                this.playerRef.GetComponent<PlayerInfo>().AddKeyCard(this.keyCard);
                // this.playerRef.GetComponent<PlayerInfo>().IsPickupActive = isPickUpActive;
                Object.Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.isPickUpActive = true;
            this.playerRef = other.gameObject;
            this.playerRef.GetComponent<PlayerInfo>().IsPickupActive = isPickUpActive;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.isPickUpActive = false;
            this.playerRef.GetComponent<PlayerInfo>().IsPickupActive = isPickUpActive;
            this.playerRef = null;
        }
    }
}

