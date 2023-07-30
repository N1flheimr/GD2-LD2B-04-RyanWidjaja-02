using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReduceFireballCost", menuName = "Upgrade/ReduceFireballCost")]
public class PlayerReduceArrowDamage : BasePlayerUpgradeSO
{
    public int amount;
    public int price;

    public override void Apply() {
        Bow playerBow = GameManager.instance.GetPlayerTransform().GetComponentInChildren<Bow>();

        if (CoinManager.Instance.CanSpendCoin(price)) {
            CoinManager.Instance.ConsumeCoin(price);
            playerBow.SetConsumeHealthAmount(playerBow.GetConsumeHealthAmount() - amount);
        }
    }
}
