using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth;

    [SerializeField] private float maxHealth = 150f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth < 0f)
        {
            currentHealth = 0f;
        }
    }

    public void SetHealth(float newHealth)
    {
        currentHealth = newHealth;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetCurrentMaxHealth()
    {
        return maxHealth;
    }
}
