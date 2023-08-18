using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHp = 5;
    public int HP = 5;

    bool invincible;
    float invincibilityTime = 0.8f;
    float blinkTime = 0.1f;

    public float knockbackStrength = 2f;
    float knockbackTime = 0.3f;

    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    EnemyHit enemyHit;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHit = GetComponentInChildren<EnemyHit>();
        HP = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon" && !invincible)
        {
            HP--;
            if (HP <= 0)
            {
                enemyHit.Defeat();
            }
            StartCoroutine(Invincibility());
            StartCoroutine(Knockback(collision.transform.position));
        }
    }

    IEnumerator Invincibility()
    {
        invincible = true;

        float auxTime = invincibilityTime;

        while (auxTime > 0)
        {
            yield return new WaitForSeconds(blinkTime);
            auxTime -= blinkTime;
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }

        spriteRenderer.enabled = true;
        invincible = false;
    }
    IEnumerator Knockback(Vector3 hitPosition)
    {
        if (knockbackStrength <= 0) yield break;
        rigidbody.velocity = (transform.position - hitPosition).normalized * knockbackStrength;
        yield return new WaitForSeconds(knockbackTime);
        rigidbody.velocity = Vector3.zero;
    }

    public void HideEnemy()
    {
        StopAllCoroutines();
        rigidbody.velocity = Vector3.zero;
        spriteRenderer.enabled = false;
    }
}
