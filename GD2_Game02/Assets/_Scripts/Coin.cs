using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool hasTriggerEntered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggerEntered)
        {
            CoinManager.Instance.AddCoinCount(1);
            hasTriggerEntered = true;
            Destroy(gameObject);
        }
    }
}
