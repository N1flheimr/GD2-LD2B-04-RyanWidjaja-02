using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    [SerializeField] private Transform wallDetection;
    [SerializeField] private GameObject rotatingSprite;

    private bool isFacingRight = true;

    private void Awake()
    {

    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        float radius = 0.1f;
        Collider2D wallCollider = Physics2D.OverlapCircle(wallDetection.position, radius, LayerMask.GetMask("Ground"));

        if (wallCollider)
        {
            Flip();
        }

        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
    }

    void Flip()
    {
        if (isFacingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        isFacingRight = !isFacingRight;
        Debug.Log("Flip");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            Health playerHealth = collision.GetComponent<Health>();
            playerHealth.TakeDamage(25f);
            Debug.Log("Damage");
        }
    }
}
