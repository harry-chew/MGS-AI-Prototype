using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLossPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI winLossText;
    [SerializeField] private GameObject winLossPanel;

    [SerializeField] private Color winColour;
    [SerializeField] private Color lossColour;
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
            winLossPanel.GetComponent<Image>().color = winColour;
            winLossText.text = "You Win";
        } else if(!obj)
        {
            Debug.Log("You Lose");
            winLossPanel.GetComponent<Image>().color = lossColour;
            winLossText.text = "You Lose";
        }

        Time.timeScale = 0f;
    }
}
