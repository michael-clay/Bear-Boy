using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

    public float speed = 0.2f;

    [SerializeField] private string itemName;

    private bool movingUp;

    private void Start()
    {
        movingUp = true;
    }

    private void Update()
    {
        if (movingUp)
        {
            transform.Translate(0, Time.deltaTime * speed, 0);
        }
        else
        {
            transform.Translate(0, -Time.deltaTime * speed, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name == "TopPosition" || other.gameObject.name == "BottomPosition")
        {
            if (movingUp)
            {
                movingUp = false;
            }
            else
            {
                movingUp = true;
            }
        }
        else if (other.gameObject.name == "BearBoy1")
        {
            other.GetComponent<InventoryController>().AddItem(itemName);

            Destroy(this.gameObject);
        }

        
    }
}
