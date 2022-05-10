using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float speed = 5;
    public float rotationSpeed = 2;
    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void RotatePlayer()
    {
        Vector3 targetDirection = target.position - transform.position;
        transform.rotation= Quaternion.LookRotation(targetDirection, Vector3.up);
    }
}
