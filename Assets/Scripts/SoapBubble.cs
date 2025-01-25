using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBubble : BasicBubble
{
    void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            Pop();
    }
}
