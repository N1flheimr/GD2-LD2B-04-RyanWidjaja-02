using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            SoundManager.PlaySound(SoundManager.SoundType.TakeDamage, 0.85f);
        }
    }
}
