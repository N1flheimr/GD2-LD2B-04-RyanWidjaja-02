using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Health playerHealth;

    private void Update()
    {
        healthText.text = playerHealth.GetCurrentHealth().ToString("000");
    }
}
