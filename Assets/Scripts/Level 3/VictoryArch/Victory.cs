using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BearBoy1")
        {
            Messenger.Broadcast(GameEvent.PLAYER_VICTORY);
        }
    }
}
