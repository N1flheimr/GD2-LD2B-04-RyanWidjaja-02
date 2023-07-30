using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject buttonGameObject;
    [SerializeField] private GameObject shopWindowGameObject;

    private bool canOpenShop;
    private bool isOpen;

    private void Start() {
        buttonGameObject.SetActive(false);
        shopWindowGameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (canOpenShop && !isOpen) {
                OpenShopWindow();
            }
            else if (isOpen) {
                CloseShopWindow();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (collision.TryGetComponent<PlayerController>(out PlayerController playerController)) {
                ActiveButton();
                canOpenShop = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (collision.TryGetComponent<PlayerController>(out PlayerController playerController)) {
                DeactiveButton();
                canOpenShop = false;
                CloseShopWindow();
            }
        }
    }
    private void ActiveButton() {
        buttonGameObject.SetActive(true);
    }

    private void DeactiveButton() {
        buttonGameObject.SetActive(false);
    }

    private void OpenShopWindow() {
        shopWindowGameObject?.SetActive(true);
        isOpen = true;
    }

    private void CloseShopWindow() {
        shopWindowGameObject?.SetActive(false);
        isOpen = false;
    }

    public bool GetIsOpen() {
        return isOpen;
    }
}
