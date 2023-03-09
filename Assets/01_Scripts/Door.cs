using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum DoorKeyCard
{
    None = 0,
    Red = 1,
    Yellow = 2,
    Blue = 3,
}

public class Door : MonoBehaviour
{
    public GameObject MovingDoor;
    public DoorKeyCard AcceptedKeyCard;
    public bool IsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsOpen)
        {
            var localPosition = new Vector3(0, 0, 0);
            var localRotation = new Quaternion(0,0,0,0);
            this.MovingDoor.transform.GetLocalPositionAndRotation(out localPosition, out localRotation);

            Debug.Log(localPosition.y);

            if (localPosition.y <= 4.25)
            {
                this.MovingDoor.transform.Translate(Vector3.up * Time.deltaTime, Space.Self);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            Debug.Log("Player in");
            Debug.Log(other.gameObject.name);

            // Get player info 
            var playerInfo = other.gameObject.GetComponent<PlayerInfo>();
            if (playerInfo == null)
            {
                Debug.Log("Player in no info");
                return;
            }

            if (playerInfo.HasKeyCard(this.AcceptedKeyCard))
            {
                Debug.Log("Player in open");
                this.IsOpen = true;
            }
        }
    }
}

