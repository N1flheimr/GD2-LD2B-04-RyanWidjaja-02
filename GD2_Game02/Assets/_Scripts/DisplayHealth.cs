using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Health playerHealth;

    private void Start()
    {
        healthText.text = playerHealth.GetCurrentHealth().ToString("000");
        playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
        playerHealth.OnMaxHealthChanged += PlayerHealth_OnMaxHealthChanged;
    }

    private void PlayerHealth_OnMaxHealthChanged(object sender, System.EventArgs e)
    {
        healthText.text = playerHealth.GetCurrentHealth().ToString("000");
    }

    private void PlayerHealth_OnHealthChanged(object sender, System.EventArgs e)
    {
        healthText.text = playerHealth.GetCurrentHealth().ToString("000");
    }

    private void OnDestroy()
    {
        playerHealth.OnHealthChanged -= PlayerHealth_OnHealthChanged;
        playerHealth.OnMaxHealthChanged -= PlayerHealth_OnMaxHealthChanged;
    }
}
