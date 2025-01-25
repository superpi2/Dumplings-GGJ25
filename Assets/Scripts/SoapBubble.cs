using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBubble : BasicBubble
{
    PlayerMovement occupant;

    void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            Pop();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sharp")
            Destroy(collision.gameObject);

        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.tag == "Player")
        {
            occupant = collision.gameObject.transform.parent.GetComponent<PlayerMovement>();
            occupant.EnterBubble(gameObject);
        }
    }

    protected override void Pop()
    {
        if (occupant != null)
            occupant.ExitBubble();

        base.Pop();
    }
}
