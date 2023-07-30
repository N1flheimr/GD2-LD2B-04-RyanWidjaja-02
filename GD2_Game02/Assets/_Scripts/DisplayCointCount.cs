using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCointCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cointCountText;


    private void Start() {
        cointCountText.text = CoinManager.Instance.GetCoinCount().ToString("000");
        CoinManager.Instance.OnCoinCollected += Instance_OnCoinCollected;
    }

    private void Instance_OnCoinCollected(object sender, int e) {
        cointCountText.text = e.ToString("000");
    }
}
