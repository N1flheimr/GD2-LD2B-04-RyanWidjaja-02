using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinManager : MonoBehaviour
{
    public event EventHandler<int> OnCoinCollected;

    public static CoinManager Instance;

    private int coinCount = 0;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void AddCoinCount(int amount) {
        coinCount += amount;
        OnCoinCollected?.Invoke(this, coinCount);
        Debug.Log("+1");
    }

    public void ConsumeCoin(int amount) {
        if (!CanSpendCoin(amount)) {
            return;
        }

        coinCount -= amount;
    }

    public bool CanSpendCoin(int amount) {
        if (coinCount - amount >= 0) {
            return true;
        }
        return false;
    }

    public int GetCoinCount() {
        return coinCount;
    }
}
