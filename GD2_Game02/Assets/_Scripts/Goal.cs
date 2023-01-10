using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!GameManager.instance.isGameClear())
            {
                SoundManager.PlaySound(SoundManager.SoundType.GameClear);
            }
            GameManager.instance.GameClear();
        }
    }
}
