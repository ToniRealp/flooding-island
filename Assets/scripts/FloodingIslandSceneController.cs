using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloodingIslandSceneController : MonoBehaviour
{
    public GameStartColliderController player1ColliderController;
    public GameStartColliderController player2ColliderController;
    public GameObject mainTitle;
    public GameObject countdownTextGameObject;

    private TextMeshProUGUI countdownText;
    

    public float timeToStart = 3;
    private void OnEnable()
    {
        EventManager.OnGameOver.AddListener(ResetGame);
    }

    private void OnDisable()
    {
        EventManager.OnGameOver.RemoveListener(ResetGame);
    }

    private void Start()
    {
        countdownText = countdownTextGameObject.GetComponent<TextMeshProUGUI>();
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
            mainTitle.SetActive(false);
            countdownTextGameObject.SetActive(false);
        }

        countdownText.text = $"{timeToStart:0.0}";

    }

    void ResetGame(EventManager.OnGameOverInfo gameOverInfo)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
