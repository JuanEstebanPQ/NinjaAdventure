using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    Vector2 direction;

    Rigidbody2D rigidbody;
    Animator animator;

    bool isAttacking;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = direction * speed;
    }

    void Update()
    {
        Movement();
        Animations();
    }

    private void Movement()
    {
        if (isAttacking) return;

        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("Attack");
            isAttacking = true;
            AttackAnimDirection();
        }
    }

    private void Animations()
    {
        if (isAttacking) return;

        if (direction.magnitude != 0)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.Play("Run");
        }
        else animator.Play("Idle");
    }

    private void AttackAnimDirection()
    {
        direction.x = animator.GetFloat("Horizontal");
        direction.y = animator.GetFloat("Vertical");

        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction = direction.normalized;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        direction = Vector2.zero;
    }

    private void EndAttack()
    {
        isAttacking = false;
    }
}
