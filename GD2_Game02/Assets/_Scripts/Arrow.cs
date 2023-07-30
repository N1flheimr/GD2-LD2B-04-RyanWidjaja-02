using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private CircleCollider2D circleColliderTrigger;

    private bool triggerEntered;

    private void Start() {
        hasHit = false;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(HealAmountDecrease());
    }

    private void Update() {

        if (outerLight.pointLightOuterRadius > 0f) {
            if (innerLight.pointLightInnerRadius > 0f) {
                innerLight.pointLightInnerRadius -= innerLightFadeSpeed * Time.deltaTime;
            }
            if (innerLight.pointLightOuterRadius > 0f) {
                innerLight.pointLightOuterRadius -= innerLightFadeSpeed * Time.deltaTime;
            }
            outerLight.pointLightOuterRadius -= outerLightFadeSpeed * Time.deltaTime;
        }
        else {
            if (hasHit) {
                innerLight.enabled = false;
                outerLight.enabled = false;
                Destroy(rb);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (rb != null) {
            if (collision.collider.CompareTag("Ground")) {
                hasHit = true;
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy") && !hasHit && circleColliderTrigger.isTrigger) {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy) && !triggerEntered) {
                enemy.Death();
                triggerEntered = true;
            }
        }

        if (collision.CompareTag("Player") && hasHit && circleColliderTrigger.isTrigger) {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth.GetCurrentHealth() == playerHealth.GetCurrentMaxHealth()) {
                return;
            }
            playerHealth.SetHealth(playerHealth.GetCurrentHealth() + healAmount);
            SoundManager.PlaySound(SoundManager.SoundType.Heal);
            Destroy(this.gameObject);
        }
    }
    public IEnumerator HealAmountDecrease() {
        while (healAmount > 5) {
            yield return new WaitForSeconds(healDecreaseTime);
            healAmount--;
        }
        StopCoroutine(HealAmountDecrease());
    }
}
