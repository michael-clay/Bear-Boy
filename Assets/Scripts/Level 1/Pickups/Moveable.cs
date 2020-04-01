using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject holdPos;
    [SerializeField] private GameObject placePos;

    private Vector3 placeSize;

    private Vector3 heldPos;
    private Vector3 placedPos;
    private Quaternion placedRot;

    private bool held;

    [SerializeField] private Vector3 heldSize;

    [SerializeField] private float duration;

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLACE_MOVEABLE, Place);
    }

    private void Start()
    {
        Messenger.AddListener(GameEvent.PLACE_MOVEABLE, Place);

        placeSize = this.transform.localScale;
        held = false;
    }

    public void Operate()
    {
        if (!held)
        {
            heldPos = holdPos.transform.position;

            StartCoroutine(Miniturize(duration, transform.position, transform.localScale, heldPos, heldSize));

            Messenger.Broadcast(GameEvent.PICKED_UP_OBJECT, false);

            held = true;
        }
    }

    public void Place()
    {
        if (held)
        {
            placedPos = placePos.transform.position;
            placedRot = placePos.transform.rotation;

            StartCoroutine(Bigify(duration, transform.position, transform.localScale, transform.rotation, placedPos, placeSize, placedRot));
        }
    }

    private IEnumerator Miniturize(float duration, Vector3 startPoint, Vector3 startSize, Vector3 endPoint, Vector3 endSize)
    {
        float time = 0;
        float distance = 0;

        while (time < duration)
        {
            distance = time / duration;
            transform.position = Vector3.Lerp(startPoint, endPoint, distance);
            transform.localScale = Vector3.Lerp(startSize, endSize, distance);

            time += Time.deltaTime;

            yield return null;

            transform.parent = player.transform;

        }
    }

    private IEnumerator Bigify(float duration, Vector3 startPoint, Vector3 startSize, Quaternion startRot, Vector3 endPoint, Vector3 endSize, Quaternion endRot)
    {
        float time = 0;
        float distance = 0;

        while (time < duration)
        {
            distance = time / duration;
            transform.position = Vector3.Lerp(startPoint, endPoint, distance);
            transform.localScale = Vector3.Lerp(startSize, endSize, distance);
            transform.rotation = Quaternion.Lerp(startRot, endRot, distance);

            time += Time.deltaTime;

            yield return null;

            transform.parent = null;

        }
    }
}
