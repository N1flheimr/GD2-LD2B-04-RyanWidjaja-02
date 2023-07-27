using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private RawImage buttonUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                playerController.GetButtonRawImage().enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                playerController.GetButtonRawImage().enabled = false;
            }
        }
    }
}
