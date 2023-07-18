using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private GameObject coinPrefab;

    public void Death()
    {
        Destroy(this.gameObject);
        SoundManager.PlaySound(SoundManager.SoundType.EnemyDeath);
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            Health playerHealth = collision.GetComponent<Health>();
            playerHealth.TakeDamage(damageAmount);
            Destroy(this.gameObject);
            SoundManager.PlaySound(SoundManager.SoundType.TakeDamage, 0.85f);
            SoundManager.PlaySound(SoundManager.SoundType.EnemyDeath);
        }
    }
}
