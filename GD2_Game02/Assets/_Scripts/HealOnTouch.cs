using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HealOnTouch : MonoBehaviour
{
    [SerializeField] private float amount;
    private bool hasTriggerEntered = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !hasTriggerEntered) {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth.GetCurrentHealth() == playerHealth.GetCurrentMaxHealth()) {
                return;
            }
            playerHealth.SetHealth(playerHealth.GetCurrentHealth() + amount);
            SoundManager.PlaySound(SoundManager.SoundType.Heal);
            hasTriggerEntered = true;
            Destroy(this.gameObject);
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Vector3 hitPosition = Vector3.zero;
    //    if(collision.collider.CompareTag("Player"))
    //    {
    //        if (tilemap != null)
    //        {
    //            foreach (ContactPoint2D hit in collision.contacts)
    //            {
    //                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
    //                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
    //                Debug.Log(hit.normal.x);
    //                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);

    //                Health playerHealth = collision.collider.GetComponent<Health>();
    //                playerHealth.SetHealth(playerHealth.GetCurrentHealth() + amount);
    //            }
    //        }
    //    }
    //}
}
