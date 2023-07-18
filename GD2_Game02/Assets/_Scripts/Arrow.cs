using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class Arrow : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool hasHit;

    [SerializeField] private Light2D innerLight;
    [SerializeField] private Light2D outerLight;
    [SerializeField] private float innerLightFadeSpeed;
    [SerializeField] private float outerLightFadeSpeed;

    [SerializeField] private float healAmount;
    [SerializeField] private float healDecreaseTime;

    private void Start()
    {
        hasHit = false;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(HealAmountDecrease());
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
            if (hasHit)
            {
                innerLight.enabled = false;
                outerLight.enabled = false;
                Destroy(rb);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            hasHit = true;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !hasHit)
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Death();
            }
        }

        if (collision.CompareTag("Player") && hasHit)
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth.GetCurrentHealth() == playerHealth.GetCurrentMaxHealth())
            {
                return;
            }
            playerHealth.SetHealth(playerHealth.GetCurrentHealth() + healAmount);
            SoundManager.PlaySound(SoundManager.SoundType.Heal);
            Destroy(this.gameObject);
        }
    }
    public IEnumerator HealAmountDecrease()
    {
        while (healAmount > 0)
        {
            yield return new WaitForSeconds(healDecreaseTime);
            healAmount--;
        }
    }
}
