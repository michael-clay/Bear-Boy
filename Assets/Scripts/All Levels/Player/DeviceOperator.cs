using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour {

    public float radius = 2f;
    private bool withinRadius;
    private Vector3 _middlePoint;

    [SerializeField] GameObject placeArea;

    private void Start()
    {
        withinRadius = false;
    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
        {
            _middlePoint = GameObject.Find("mixamorig:Hips").transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(_middlePoint, radius);
            
            foreach (Collider hitCollider in hitColliders)
            {
                hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);                
            }
        }
        else if (Input.GetButtonDown("Fire3") && withinRadius)
        {
            Messenger.Broadcast(GameEvent.PLACE_MOVEABLE);
        }
	}

    public void WithinRadius(bool indicator)
    {
        withinRadius = indicator;
    }
}
