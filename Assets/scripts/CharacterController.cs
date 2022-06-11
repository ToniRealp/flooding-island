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

    public AudioManager audioManager;

    private bool _onSolidGround = true;
    private bool isMoving = false;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
        {
            _animator.SetBool(IsMoving, true);
            if (!audioManager.IsPlaying("Walk"))
            {
                audioManager.Play("Walk");
            }
        }
        else
        {
            _animator.SetBool(IsMoving, false);
            audioManager.Stop("Walk");
        }
    }

    private void FixedUpdate()
    {
        
            MovePlayer();
            _onSolidGround = false;
    }

    void MovePlayer()
    {
        var targetPosition = new Vector3(target.position.x, 0, target.position.z);
        if ((targetPosition - transform.position).magnitude > 5 && _onSolidGround)
        {
            var targetDirection = (targetPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            _rigidbody.MovePosition(transform.position + transform.forward * (velocity * Time.fixedDeltaTime));
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
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
            audioManager.Play("Splash");
        }
    }
}