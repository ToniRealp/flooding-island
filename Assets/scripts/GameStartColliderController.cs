using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartColliderController : MonoBehaviour
{
    public string playerCursorTag;
    public bool playerReady = false;
    private GameObject particleSystem;

    
    private void OnEnable()
    {
        EventManager.OnGameStart.AddListener(() => { particleSystem.SetActive(false);});
    }

    private void OnDisable()
    {
        EventManager.OnGameStart.RemoveListener(() => { particleSystem.SetActive(false);});
    }
    
    private void Start()
    {
        particleSystem = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerCursorTag))
        {
            playerReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerCursorTag))
        {
            playerReady = false;
        }
    }
}
