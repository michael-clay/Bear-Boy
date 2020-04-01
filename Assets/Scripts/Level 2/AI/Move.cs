using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [SerializeField] Transform[] keyDestinations;

    private int curKeyDes;
    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        Messenger.AddListener(GameEvent.FINISHED_TALKING, GoToNextKey);
        Messenger.AddListener<int>(GameEvent.UPDATE_KEY_LOCATION, UpdateKeyLocation);

        agent = GetComponent<NavMeshAgent>();

        curKeyDes = 0;
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.FINISHED_TALKING, GoToNextKey);
        Messenger.RemoveListener<int>(GameEvent.UPDATE_KEY_LOCATION, UpdateKeyLocation);
    }

    private void UpdateKeyLocation(int key)
    {
        curKeyDes = key;
    }

    public void Operate()
    {
        Messenger.Broadcast(GameEvent.TALKED_TO_HELPER);
    }

    public void GoToNextKey()
    {
        if (curKeyDes < keyDestinations.Length)
        {
            agent.destination = keyDestinations[curKeyDes].position;
            curKeyDes += 1;
        }
    }
}
