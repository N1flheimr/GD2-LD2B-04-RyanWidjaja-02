using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnMaxHealthChanged;

    [SerializeField] private float currentHealth;

    [SerializeField] private float maxHealth = 150f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Death();
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetHealth(float newHealth)
    {
        currentHealth = newHealth;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetCurrentMaxHealth()
    {
        return maxHealth;
    }

    public void Death()
    {
        if (!GameManager.instance.IsGameClear())
        {
            gameObject.SetActive(false);
            GameManager.instance.GameOver();
        }
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnMaxHealthChanged?.Invoke(this, EventArgs.Empty);
    }
}
