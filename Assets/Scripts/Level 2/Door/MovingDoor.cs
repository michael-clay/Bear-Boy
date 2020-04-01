using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : MonoBehaviour {

    
    [SerializeField] private float duration;

    private Vector3 openPos;
    private Vector3 closePos;

    private void Start()
    {
        closePos = transform.position;
        openPos = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
    }

    public void OpenDoor()
    {
        StartCoroutine(Move(duration, transform.position, openPos));
    }

    public void CloseDoor()
    {
        StartCoroutine(Move(duration, transform.position, closePos));
    }

    private IEnumerator Move(float duration, Vector3 startPoint, Vector3 endPoint)
    {
        float time = 0;
        float distance = 0;

        while (time < duration)
        {
            distance = time / duration;
            transform.position = Vector3.Lerp(startPoint, endPoint, distance);

            time += Time.deltaTime;

            yield return null;
        }
    }
}
