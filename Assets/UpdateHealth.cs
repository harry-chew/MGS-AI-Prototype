using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHealth : MonoBehaviour
{
    private TMPro.TextMeshProUGUI healthText;
    private void Start()
    {
        healthText = GetComponent<TMPro.TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        PlayerMove.OnHealthChange += UpdateHealthUI;
    }

    private void OnDisable()
    {
        PlayerMove.OnHealthChange -= UpdateHealthUI;
    }

    private void UpdateHealthUI(int obj)
    {
        healthText.text = "Health: " + obj;
    }
}
