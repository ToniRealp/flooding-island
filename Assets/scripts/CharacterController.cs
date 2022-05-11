using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private new Rigidbody _rigidbody;
    public Transform target;
    public float targetVelocity = 10;

    public float forceStrength = 500;
    // public Vector3 targetSpeed = new Vector3(5,0,5);
  
    private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    private bool onSolidGround = true;


    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if ((target.position - transform.position).magnitude > 5)
        {
            _animator.SetBool(IsMoving, true);
        }
        else
        {
            _animator.SetBool(IsMoving, false);
        }
    }

    private void FixedUpdate()
    {
        if ((target.position - transform.position).magnitude > 5 && onSolidGround)
        {
            MovePlayer();
        }

        onSolidGround = false;
    }

    void MovePlayer()
    {
        var targetDirection = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        _rigidbody.MovePosition(transform.position + transform.forward * (targetVelocity * Time.fixedDeltaTime));
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Ground"))
        {
            onSolidGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SinkTrigger"))
        {
            _animator.SetTrigger("isFalling");
        }
    }
}