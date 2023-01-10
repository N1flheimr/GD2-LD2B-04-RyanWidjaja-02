using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform respawnPoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameClearScreen;
    [SerializeField] private Transform treasureTransform;

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

    public bool isGameClear()
    {
        return gameClearScreen.gameObject.activeInHierarchy;
    }
}
