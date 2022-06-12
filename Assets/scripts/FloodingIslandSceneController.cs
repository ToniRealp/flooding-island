using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloodingIslandSceneController : MonoBehaviour
{
    public GameStartColliderController player1ColliderController;
    public GameStartColliderController player2ColliderController;

    public float timeToStart = 3;
    private void OnEnable()
    {
        EventManager.OnGameOver.AddListener(ResetGame);
    }

    private void OnDisable()
    {
        EventManager.OnGameOver.RemoveListener(ResetGame);
    }

    private void Update()
    {
        if (player1ColliderController.playerReady && player2ColliderController.playerReady && timeToStart > 0)
        {
            timeToStart -= Time.deltaTime;
        }

        if (timeToStart <= 0)
        {
            EventManager.OnGameStart.Invoke();
        }
        
    }

    void ResetGame(EventManager.OnGameOverInfo gameOverInfo)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
