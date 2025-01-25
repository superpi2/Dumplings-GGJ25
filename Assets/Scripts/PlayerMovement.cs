using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animator;

    public float targetVelocity;

    bool grounded;
    bool wallLeft;
    bool wallRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(2f, 0f);
    }

    private void Update()
    {
        rb.velocity = new Vector2(targetVelocity, rb.velocity.y);

        PhysicsLoop();

        if (wallLeft)
            targetVelocity = Mathf.Abs(targetVelocity);
        else if (wallRight)
            targetVelocity = -Mathf.Abs(targetVelocity);

        sprite.flipX = targetVelocity < 0f;
        animator.SetBool("Falling", !grounded);
    }

    void PhysicsLoop()
    {
        Vector2 origin = (Vector2)transform.position + new Vector2(0f, -0.6f);
        Vector2 size = new Vector2(0.6f, 1 / 32f);

        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            origin, size, 0f, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(origin - size / 2f, size, Color.red);

        grounded = hits.Length > 0;

        origin = (Vector2)transform.position + new Vector2(-0.3f, 0f);
        size = new Vector2(1 / 32f, 1f);

        wallLeft = false;

        hits = Physics2D.BoxCastAll(
            origin, size, 0f, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(origin - size / 2f, size, Color.green);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && !hit.collider.usedByEffector)
            {
                wallLeft = true;
                break;
            }
        }

        wallRight = false;

        origin = (Vector2)transform.position + new Vector2(0.3f, 0f);

        hits = Physics2D.BoxCastAll(
            origin, size, 0f, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(origin - size / 2f, size, Color.green);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && !hit.collider.usedByEffector)
            {
                wallRight = true;
                break;
            }
        }
    }
}
