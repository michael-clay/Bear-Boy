using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HStairs : MonoBehaviour {

    [SerializeField] private Vector3 outPos;
    [SerializeField] private float duration;

    public void RevealStairs()
    {
        StartCoroutine(MoveStairs(duration, transform.position, outPos));
    }

    private IEnumerator MoveStairs(float duration, Vector3 startPoint, Vector3 endPoint)
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
