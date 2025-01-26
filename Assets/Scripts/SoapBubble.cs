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
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "FrozenBubble")
        {
            if (collision.collider.usedByEffector)
                return;

            Pop();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sharp")
            Destroy(collision.gameObject);

        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.tag == "Player" && occupant == null)
        {
            occupant = collision.gameObject.transform.parent.GetComponent<PlayerMovement>();
            bool v = occupant.EnterBubble(gameObject);
            if (!v) occupant = null;
        }
    }

    protected override void Pop()
    {
        if (occupant != null)
            occupant.ExitBubble();

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        base.Pop();
    }
}
