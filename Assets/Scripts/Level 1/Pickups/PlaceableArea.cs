using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableArea : MonoBehaviour {

    [SerializeField] GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<DeviceOperator>().WithinRadius(true);

        Messenger.Broadcast(GameEvent.PLACE_OBJECT, true);
    }

    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<DeviceOperator>().WithinRadius(false);

        Messenger.Broadcast(GameEvent.PLACE_OBJECT, false);
    }
}
