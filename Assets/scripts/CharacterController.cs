using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Transform target;
    public float velocity = 10;

    private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");

    private bool _onSolidGround = true;

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
        if ((target.position - transform.position).magnitude > 5 && _onSolidGround)
        {
            MovePlayer();
        }

        _onSolidGround = false;
    }

    void MovePlayer()
    {
        var targetDirection = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        _rigidbody.MovePosition(transform.position + transform.forward * (velocity * Time.fixedDeltaTime));
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Ground"))
        {
            _onSolidGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SinkTrigger"))
        {
            _animator.SetTrigger(IsFalling);
        }
    }
}