using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class GameManager : MonoBehaviour
{
    public Transform respawnPoint;
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        playerTransform.position = respawnPoint.position;
    }
}
