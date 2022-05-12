using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloodingIslandSceneController : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnGameOver.AddListener(ResetGame);
    }

    private void OnDisable()
    {
        EventManager.OnGameOver.RemoveListener(ResetGame);
    }

    void ResetGame(EventManager.OnGameOverInfo gameOverInfo)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
