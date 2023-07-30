using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Health playerHealth;
    [SerializeField] private RectTransform UIhealthTransform;

    [SerializeField] private Image healthBarImage;

    private float UIHealthSizeY;

    private void Start() {
        healthText.text = playerHealth.GetCurrentHealth().ToString("00");
        playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
        playerHealth.OnMaxHealthChanged += PlayerHealth_OnMaxHealthChanged;

        UIHealthSizeY = UIhealthTransform.sizeDelta.y;
    }

    private void PlayerHealth_OnMaxHealthChanged(object sender, System.EventArgs e) {
        healthText.text = playerHealth.GetCurrentHealth().ToString("00");
    }

    private void PlayerHealth_OnHealthChanged(object sender, System.EventArgs e) {
        healthText.text = playerHealth.GetCurrentHealth().ToString("00");
        Vector2 healthImageSize = new Vector2(UIhealthTransform.sizeDelta.x, UIHealthSizeY * (playerHealth.GetCurrentHealth() / playerHealth.GetCurrentMaxHealth()));
        healthBarImage.fillAmount = playerHealth.GetCurrentHealth() / playerHealth.GetCurrentMaxHealth();
        UIhealthTransform.sizeDelta = healthImageSize;
    }

    private void OnDestroy() {
        playerHealth.OnHealthChanged -= PlayerHealth_OnHealthChanged;
        playerHealth.OnMaxHealthChanged -= PlayerHealth_OnMaxHealthChanged;
    }
}
