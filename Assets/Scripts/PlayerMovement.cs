using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    bool grounded;
    bool wallLeft;
    bool wallRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(2f, 0f);
    }

    private void Update()
    {
        PhysicsLoop();

        if (wallLeft)
            rb.velocity = new Vector2(2f, 0f);
        else if (wallRight)
            rb.velocity = new Vector2(-2f, 0f);
    }

    void PhysicsLoop()
    {
        Vector2 origin = (Vector2)transform.position + new Vector2(0f, -0.5f);
        Vector2 size = new Vector2(0.5f, 1 / 32f);

        RaycastHit2D[] hit = Physics2D.BoxCastAll(
            origin, size, 0f, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(origin - size / 2f, size, Color.red);

        grounded = hit.Length > 0;

        origin = (Vector2)transform.position + new Vector2(-0.25f, 0f);
        size = new Vector2(1 / 32f, 0.9f);

        hit = Physics2D.BoxCastAll(
            origin, size, 0f, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(origin - size / 2f, size, Color.green);

        wallLeft = hit.Length > 0;

        origin = (Vector2)transform.position + new Vector2(0.25f, 0f);
        size = new Vector2(1 / 32f, 0.9f);

        hit = Physics2D.BoxCastAll(
            origin, size, 0f, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(origin - size / 2f, size, Color.green);

        wallRight = hit.Length > 0;
    }
}
