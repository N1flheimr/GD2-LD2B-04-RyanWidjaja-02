using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject fireballPrefab;

    [SerializeField] private float launchForce;

    [SerializeField] private float chargeSpeed;

    [SerializeField] private float maxLaunchForce;

    [SerializeField] private float consumeHealthAmount;

    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject pointParentGameObject;

    private GameObject arrowGameObject;

    private bool isCharging;

    private Health playerHealth;

    [System.NonSerialized] public Vector2 direction;

    private void Awake()
    {
        playerHealth = GetComponentInParent<Health>();
        isCharging = false;
    }

    private void Update()
    {
        Vector2 position = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = (mousePos - position).normalized;
        transform.right = direction;

        Shoot();
    }

    private void Charge()
    {
        if (!isCharging)
        {
            isCharging = true;
            arrowGameObject = Instantiate(fireballPrefab, shotPoint.position, shotPoint.rotation);
            arrowGameObject.transform.parent = shotPoint;
            arrowGameObject.GetComponent<Rigidbody2D>().isKinematic = true;

            CircleCollider2D[] circleCollider2D = arrowGameObject.GetComponents<CircleCollider2D>();
            foreach(CircleCollider2D circleColliders in circleCollider2D)
            {
                circleColliders.enabled = false;
            }
            SoundManager.PlaySound(SoundManager.SoundType.FireballCharge);
            pointParentGameObject.SetActive(true);
        }

        launchForce += chargeSpeed * Time.deltaTime;

        if (launchForce > maxLaunchForce)
        {
            launchForce = maxLaunchForce;
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            Charge();
        }

        if (Input.GetMouseButtonUp(0))
        {
            arrowGameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            arrowGameObject.GetComponent<CircleCollider2D>().enabled = true;
            arrowGameObject.transform.parent = null;

            if (arrowGameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.velocity = transform.right * launchForce;
            }
            isCharging = false;
            launchForce = 0;
            pointParentGameObject.SetActive(false);
            arrowGameObject = null;
            SoundManager.PlaySound(SoundManager.SoundType.BowRelease, 0.85f);
            ConsumeHealth();
        }
    }

    private void ConsumeHealth()
    {
        playerHealth.TakeDamage(consumeHealthAmount);
    }

    public float GetLaunchForce()
    {
        return launchForce;
    }

    public Transform GetShotPoint()
    {
        return shotPoint;
    }

    public GameObject GetPointParentGameObject()
    {
        return pointParentGameObject;
    }
}
