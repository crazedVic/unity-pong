using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI playerScoreLabel;

    [SerializeField]
    TextMeshProUGUI computerScoreLabel;

    private void Awake()
    {
        GameManager.OnPlayerScoredEvent += OnPlayerScored;
        GameManager.OnComputerScoredEvent += OnComputerScored;
        GameManager.OnScoreReset += OnScoreReset;
    }

    private void OnScoreReset()
    {
        playerScoreLabel.text = GameManager.playerScore.ToString();
        computerScoreLabel.text = GameManager.computerScore.ToString();
    }

    private void OnPlayerScored(int score)
    {
        playerScoreLabel.text = score.ToString();
    }

    private void OnComputerScored(int score)
    {
        computerScoreLabel.text = score.ToString();
    }
}
