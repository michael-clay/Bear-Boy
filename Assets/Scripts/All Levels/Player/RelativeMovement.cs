﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour {

    [SerializeField] private Transform target;

    private Animator _animator;
    private CharacterController _charController;
    private ControllerColliderHit _contact;
    private Vector3 _middlePoint;
    private float _vertSpeed;
    private bool lockMovement;

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    public float pushForce = 3.0f;

    void Start()
    {
        Messenger.AddListener(GameEvent.LOCK_CONTROLS, LockControls);
        Messenger.AddListener(GameEvent.UNLOCK_CONTROLS, UnLockControls);

        _vertSpeed = minFall;
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _middlePoint = GameObject.Find("mixamorig:Hips").transform.position;
        lockMovement = false;
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.LOCK_CONTROLS, LockControls);
        Messenger.RemoveListener(GameEvent.UNLOCK_CONTROLS, UnLockControls);
    }

    // Update is called once per frame
    void Update() {
        if (!lockMovement)
        {
            Vector3 movement = Vector3.zero;
            float horInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");
            bool hitGround = false;
            RaycastHit hit;

            if (_vertSpeed < 0 && Physics.Raycast(_middlePoint, Vector3.down, out hit))
            {
                float check = (_charController.height + _charController.radius) / 1.9f;

                hitGround = hit.distance <= check;
            }

            if (horInput != 0 || vertInput != 0)
            {
                movement.x = horInput * moveSpeed;
                movement.z = vertInput * moveSpeed;
                movement = Vector3.ClampMagnitude(movement, moveSpeed);

                Quaternion tmp = target.rotation;
                target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
                movement = target.TransformDirection(movement);
                target.rotation = tmp;

                Quaternion direction = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
            }

            _animator.SetFloat("Speed", movement.sqrMagnitude);

            if (hitGround)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.Log("HA you tried to jump");
                }
                else
                {
                    _vertSpeed = gravity;
                    _animator.SetBool("Jumping", false);
                }
            }
            else
            {
                _vertSpeed += gravity * 5 * Time.deltaTime;

                if (_vertSpeed < terminalVelocity)
                {
                    _vertSpeed = terminalVelocity;
                }

                if (_contact != null)
                {
                    _animator.SetBool("Jumping", true);
                }

                if (_charController.isGrounded)
                {
                    if (Vector3.Dot(movement, _contact.normal) < 0)
                    {
                        movement = _contact.normal * moveSpeed;
                    }
                    else
                    {
                        movement += _contact.normal * moveSpeed;
                    }
                }
            }
            movement.y = _vertSpeed;
            movement *= Time.deltaTime;
            _charController.Move(movement);
        }
	}

    private void LockControls()
    {
        lockMovement = true;
    }

    private void UnLockControls()
    {
        lockMovement = false;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;

        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
