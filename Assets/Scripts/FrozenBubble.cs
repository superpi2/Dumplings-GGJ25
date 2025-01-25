using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenBubble : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 vel = rb.velocity;

        if (Mathf.Abs(rb.velocity.y) > 0.01f)
            vel.x = 0f;
        vel.y = Mathf.Min(vel.y, 0f);

        rb.velocity = vel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sharp")
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody2D orb))
            {
                orb.velocity = Vector2.zero;
                orb.gravityScale = 0f;
            }
            Destroy(collision.gameObject, 0.25f);
        }
    }
}
