using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {

    public float speed;

    private Rigidbody rb;
    private Vector3 force;
    private bool gameStarted;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BearBoy1")
        {
            Messenger.Broadcast(GameEvent.HIT_PLAYER);

            Time.timeScale = 0;
        }
    }


    private void Start()
    {
        Messenger.AddListener(GameEvent.RELEASE_THE_BALL, StartGame);

        rb = GetComponent<Rigidbody>();
        force = this.transform.forward;

        gameStarted = false;
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.RELEASE_THE_BALL, StartGame);
    }

    private void Update()
    {
        if (gameStarted)
        {
            PushBoulder();
        }
    }

    private void StartGame()
    {
        gameStarted = true;
    }

    private void PushBoulder()
    {
        Debug.Log(force.ToString());
        rb.AddForce(force * speed);
    }
}
