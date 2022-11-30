using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasHit;
    private CircleCollider2D circleCollider2D;
    [SerializeField] private Light2D innerLight;
    [SerializeField] private Light2D outerLight;
    [SerializeField] private float innerLightFadeSpeed;
    [SerializeField] private float outerLightFadeSpeed;

    private void Start()
    {
        hasHit = false;
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (outerLight.pointLightOuterRadius > 0f)
        {
            if (innerLight.pointLightInnerRadius > 0f)
            {
                innerLight.pointLightInnerRadius -= innerLightFadeSpeed * Time.deltaTime;
            }
            if (innerLight.pointLightOuterRadius > 0f)
            {
                innerLight.pointLightOuterRadius -= innerLightFadeSpeed * Time.deltaTime;
            }
            outerLight.pointLightOuterRadius -= outerLightFadeSpeed * Time.deltaTime;
        }
        else
        {
            innerLight.enabled = false;
            outerLight.enabled = false;
            Destroy(rb);
        }

        if (!hasHit)
        {
            return;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            hasHit = true;
            circleCollider2D.enabled = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
