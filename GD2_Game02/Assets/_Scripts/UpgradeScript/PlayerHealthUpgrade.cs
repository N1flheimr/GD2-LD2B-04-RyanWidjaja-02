using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUpgrade", menuName = "Upgrade/HealthUpgrade")]
public class PlayerHealthUpgrade : BasePlayerUpgradeSO
{
    public float Amount;
    public int price;

    public override void Apply() {
        if (!CoinManager.Instance.CanSpendCoin(price)) {
            return;
        }
        Transform playerTransform = GameManager.instance.GetPlayerTransform();
        if (playerTransform.TryGetComponent<Health>(out var playerHealth)) {
            CoinManager.Instance.ConsumeCoin(price);
            playerHealth.IncreaseMaxHealth(Amount);
            Debug.Log("Max Health Increased" + playerHealth.GetCurrentMaxHealth());
        }
    }
}
