using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [Header ("EnemyHP parameters")]

    public int maxHp = 5;
    public int HP = 5;

    protected bool invincible;
    protected float invincibilityTime = 0.8f;
    protected float blinkTime = 0.1f;

    public float knockbackStrength = 2f;
    protected float knockbackTime = 0.3f;

    protected Rigidbody2D rigidbody;
    protected SpriteRenderer spriteRenderer;
    protected EnemyHit enemyHit;

    public virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHit = GetComponentInChildren<EnemyHit>();
        HP = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && !invincible)
        {
            HP--;
            if (HP <= 0)
            {
                enemyHit.Defeat();
            }
            StopBehaviour();
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
        if (knockbackStrength <= 0) 
        {
            if(HP > 0)ContinueBehaviour(); 
            yield break;
        }
        rigidbody.velocity = (transform.position - hitPosition).normalized * knockbackStrength;
        yield return new WaitForSeconds(knockbackTime);
        rigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(knockbackTime);
        if(HP > 0)ContinueBehaviour(); 
    }

    public void HideEnemy()
    {
        StopAllCoroutines();
        rigidbody.velocity = Vector3.zero;
        spriteRenderer.enabled = false;
    }

    public virtual void StopBehaviour()
    {

    }

    public virtual void ContinueBehaviour()
    {
        
    }
}
