using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUpgrade", menuName = "Upgrade/HealthUpgrade")]
public class PlayerHealthUpgrade : BasePlayerUpgradeSO
{
    public float Amount;

    public override void Apply()
    {
        Transform playerTransform = GameManager.instance.GetPlayerTransform();
        if(playerTransform.TryGetComponent<Health>(out var playerHealth))
        {
            playerHealth.IncreaseMaxHealth(Amount);
            Debug.Log("Max Health Increased" + playerHealth.GetCurrentMaxHealth());
        }
    }
}
