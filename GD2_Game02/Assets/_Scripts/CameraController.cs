using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector2 maxDistance;

    //[SerializeField] private CinemachineVirtualCamera CinemachineVirtualCamera;
    private void Awake()
    {
        transform.parent = null;
    }

    private void Update()
    {
        float distance = Vector2.Distance(playerTransform.position, transform.position);

        Vector3 mousePos = Input.mousePosition;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPosition.z = Camera.main.nearClipPlane;


        float playerOffsetY = 0.8f;

        mouseWorldPosition.x = Mathf.Max(playerTransform.position.x - maxDistance.x, mouseWorldPosition.x);
        mouseWorldPosition.x = Mathf.Min(playerTransform.position.x + maxDistance.x, mouseWorldPosition.x);
        mouseWorldPosition.y = Mathf.Max((playerTransform.position.y + playerOffsetY) - maxDistance.y, mouseWorldPosition.y);
        mouseWorldPosition.y = Mathf.Min((playerTransform.position.y + playerOffsetY) + maxDistance.y, mouseWorldPosition.y);

        transform.position = mouseWorldPosition;

        //if (transform.position.x > targetTransform.position.x + maxDistance.x)
        //{
        //    Vector3 playerTransform = transform.position;
        //    playerTransform.x = targetTransform.position.x + maxDistance.x;
        //    transform.position = playerTransform;
        //    Debug.Log("Max");
        //}
    }
}
