using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject fireballPrefab;
    [SerializeField] private float launchForce;
    [SerializeField] private Transform shotPoint;

    private void Update()
    {
        Vector2 position = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - position).normalized;
        transform.right = direction;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject arrowGameObject = Instantiate(fireballPrefab, shotPoint.position, shotPoint.rotation);

        if(arrowGameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = transform.right * launchForce;
        }
    }
}
