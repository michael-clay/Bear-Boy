using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTable : MonoBehaviour {

    [SerializeField] private Vector3 upPos;
    [SerializeField] private float duration;

    private Vector3 downPos;

    private void Start()
    {
        downPos = this.transform.position;
        this.GetComponent<Table>().Used = true;
    }

    public void RevealTable()
    {
        StartCoroutine(Move(duration, transform.position, upPos));
        this.GetComponent<Table>().Used = false;
    }

    public void HideTable()
    {
        StartCoroutine(Move(duration, transform.position, downPos)); 
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
