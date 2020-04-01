using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {

    [SerializeField] private Transform target;

    public float rotSpeed = 1.5f;

    private float _rotY;
    private Vector3 _offset;
    private bool lockControls;

	// Use this for initialization
	void Start () {

        Messenger.AddListener(GameEvent.LOCK_CONTROLS, LockControls);
        Messenger.AddListener(GameEvent.UNLOCK_CONTROLS, UnLockControls);

        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;

        lockControls = false;
	}

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.LOCK_CONTROLS, LockControls);
        Messenger.RemoveListener(GameEvent.UNLOCK_CONTROLS, UnLockControls);
    }

    // Update is called once per frame
    void LateUpdate () {

        if (!lockControls)
        {

            float horInput = Input.GetAxis("Horizontal");

            if (horInput != 0)
            {
                _rotY += horInput * rotSpeed;

            }
            else
            {
                _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
            }

            Quaternion rotation = Quaternion.Euler(0, _rotY, 0);

            transform.position = target.position - (rotation * _offset);
            transform.LookAt(target);
        }
	}

    private void LockControls ()
    {
        lockControls = true;
    }

    private void UnLockControls()
    {
        lockControls = false;
    }
}
