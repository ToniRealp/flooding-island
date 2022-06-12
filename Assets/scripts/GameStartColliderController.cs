using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartColliderController : MonoBehaviour
{
    public string playerCursorTag;
    public bool playerReady = false;
    
    private GameObject particleSystemActive;
    private GameObject particleSystemInactive;
    
    
    private void OnEnable()
    {
        EventManager.OnGameStart.AddListener(() =>
        {
            particleSystemActive.SetActive(false);
            particleSystemInactive.SetActive(false);
            
        });
    }

    private void OnDisable()
    {
        EventManager.OnGameStart.RemoveListener(() =>
        {
            particleSystemActive.SetActive(false);
            particleSystemInactive.SetActive(false);
        });
    }
    
    private void Start()
    {
        particleSystemActive = gameObject.transform.GetChild(0).gameObject;
        particleSystemInactive = gameObject.transform.GetChild(1).gameObject;
        particleSystemActive.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerCursorTag))
        {
            playerReady = true;
            particleSystemActive.SetActive(true);
            particleSystemInactive.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerCursorTag))
        {
            playerReady = false;
            particleSystemActive.SetActive(false);
            particleSystemInactive.SetActive(true);
        }
    }
}
