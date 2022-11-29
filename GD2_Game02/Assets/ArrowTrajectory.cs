using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrajectory : MonoBehaviour
{
    [SerializeField] private GameObject pointGameObject;
    private GameObject[] pointGameObjects;
    [SerializeField] private int numberOfPoints;
    [SerializeField] private float spaceBetweenPoints;

    private Bow bow;

    private void Awake()
    {
        bow = GetComponent<Bow>();

        pointGameObjects = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            pointGameObjects[i] = Instantiate(pointGameObject, bow.GetShotPoint().position, Quaternion.identity);
            pointGameObjects[i].transform.parent = bow.GetPointParentGameObject().transform;
        }
        bow.GetPointParentGameObject().SetActive(false);
    }

    private void Update()
    {
        if (bow.GetPointParentGameObject().activeInHierarchy)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                pointGameObjects[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }
        }
    }
    private Vector2 PointPosition(float t)
    {
        float arrowGravityScale = bow.fireballPrefab.GetComponent<Rigidbody2D>().gravityScale;
        Vector2 position =
            (Vector2)bow.GetShotPoint().position + (bow.direction * bow.GetLaunchForce() * t) + 0.5f * (Physics2D.gravity * arrowGravityScale) * (t * t);

        return position;
    }
}
