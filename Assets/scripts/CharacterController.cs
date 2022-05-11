using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private new Rigidbody _rigidbody;
    public Transform target;
    public float speed = 5;
  
    private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");


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
        if ((target.position - transform.position).magnitude > 5)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        var targetDirection = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        _rigidbody.MovePosition(transform.position + transform.forward * (speed * Time.fixedDeltaTime));

    }
}