using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnCoinIncease;

    public static GameManager instance;

    public Transform respawnPoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameClearScreen;
    [SerializeField] private Transform treasureTransform;

    [SerializeField] private int coinCount;

    private void Awake()
    {
        SoundManager.Initialize();
        instance = this;
    }

    private void Start()
    {
        playerTransform.position = respawnPoint.position;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void GameClear()
    {
        gameClearScreen.SetActive(true);
    }

    public bool IsGameClear()
    {
        return gameClearScreen.gameObject.activeInHierarchy;
    }

    public void AddCoinCount(int amount)
    {
        coinCount += amount;
        OnCoinIncease?.Invoke(this, EventArgs.Empty);
    }

    public void ConsumeCoin(int amount)
    {
        coinCount -= amount;
    }
}
