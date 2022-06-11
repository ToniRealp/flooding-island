using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTileController : MonoBehaviour
{

    public GameObject solid;
    public GameObject island;
    public GameObject waterSplashEffect;
    public GameObject groundDustEffect;
    public GameObject groundBreakEffect;
    
    public float timeToBreak = 6;
    private bool _playerOnTop = false;

    private AudioManager _audioManager;

    enum Stages
    {
        Solid, Island
    }

    private Stages _currentStage = Stages.Solid;
    
    void Start()
    {
        island = gameObject.transform.GetChild(0).gameObject;
        solid = gameObject.transform.GetChild(1).gameObject;
        
        groundDustEffect = gameObject.transform.GetChild(2).gameObject;
        groundDustEffect.SetActive(false);
        
        _audioManager = FindObjectOfType<AudioManager>();
    }
    
    void Update()
    {
        if (_playerOnTop)
        {
            timeToBreak -= Time.deltaTime;
            groundDustEffect.SetActive(true);
            
            if (timeToBreak <= 0)
            {
                Vector3 spawnPoint = transform.position;
                spawnPoint.y += 5;
                
                switch (_currentStage)
                {
                    case Stages.Solid:
                        _currentStage = Stages.Island;
                        Instantiate(groundBreakEffect, spawnPoint, Quaternion.Euler(90,0,0));
                        solid.SetActive(false);
                        _audioManager.Play("Break");
                        break;
                    case Stages.Island:
                        island.SetActive(false);
                        _audioManager.Play("Break");
                        _audioManager.Stop("Rumble");
                        Instantiate(waterSplashEffect, spawnPoint, Quaternion.Euler(90,0,0));
                        Destroy(gameObject);
                        break;
                }

                timeToBreak = 3;
            }
        }
        else
        {
            groundDustEffect.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerOnTop = true;
           _audioManager.Play("Rumble");
        }
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_audioManager.IsPlaying("Rumble"))
            {
                _audioManager.Play("Rumble");    
            }
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerOnTop = false;
            _audioManager.Stop("Rumble");
        }
    }
}
