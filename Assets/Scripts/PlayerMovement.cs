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
            direction = Vector2.zero;
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

    private void EndAttack()
    {
        isAttacking = false;
    }
}
