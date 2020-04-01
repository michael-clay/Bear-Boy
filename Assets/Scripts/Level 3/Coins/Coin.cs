using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0, 0, 1);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BearBoy1")
        {
            Messenger.Broadcast(GameEvent.COLLECTED_COINS);

            Destroy(this.gameObject);
        }        
    }
}
