using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLossPanel : MonoBehaviour
{
    public TMPro.TextMeshProUGUI winLossText;
    public GameObject winLossPanel;
    private void OnEnable()
    {
        PlayerMove.OnGameWin += HandleGameWinLoss;
    }

    private void OnDisable()
    {
        PlayerMove.OnGameWin -= HandleGameWinLoss;
    }

    private void Start()
    {
        winLossPanel.SetActive(false);
    }
    private void HandleGameWinLoss(bool obj)
    {
        winLossPanel.SetActive(true);
        if (obj)
        {
            Debug.Log("You Win");
            
            winLossText.text = "You Win";
        } else if(!obj)
        {
            Debug.Log("You Lose");
            winLossText.text = "You Lose";
        }

        Time.timeScale = 0f;
    }
}
