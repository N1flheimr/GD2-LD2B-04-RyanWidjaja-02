using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera CinemachineVirtualCamera;
    [SerializeField] private Transform targetTransform;

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        targetTransform.position = Camera.main.WorldToScreenPoint(mousePos);
    }
}
