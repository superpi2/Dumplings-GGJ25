using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBubble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sharp")
            Destroy(gameObject, 0.1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Freeze")
            GameManager.instance.TransformBubble(gameObject, GameManager.instance.frozenBubble);
    }
}
