using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    [SerializeField] private GameObject target;
    [SerializeField] private string key;

    private InventoryController inventory;

    public bool requireKey;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (requireKey && !inventory.HasItem(key))
        {
            return;
        }
        else
        {
            target.SendMessage("OpenDoor");
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        target.SendMessage("CloseDoor");
    }
}
