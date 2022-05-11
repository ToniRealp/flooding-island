using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTileController : MonoBehaviour
{

    public GameObject solid;
    public GameObject island;
    
    public float timeToBreak = 3;
    private bool _playerOnTop = false;

    enum Stages
    {
        Solid, Island, Destroyed
    }

    private Stages _currentStage = Stages.Solid;
    
    // Start is called before the first frame update
    void Start()
    {
        solid = gameObject.transform.GetChild(1).gameObject;
        island = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerOnTop)
        {
            timeToBreak -= Time.deltaTime;
            
            if (timeToBreak <= 0)
            {
                switch (_currentStage)
                {
                    case Stages.Solid:
                        _currentStage = Stages.Island;
                        solid.SetActive(false);
                        break;
                    case Stages.Island:
                        _currentStage = Stages.Destroyed;
                        island.SetActive(false);
                        break;
                    case Stages.Destroyed:
                        gameObject.GetComponent<Collider>().enabled = false;
                        break;
                }

                timeToBreak = 3;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerOnTop = true;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerOnTop = false;
        }
    }
}
