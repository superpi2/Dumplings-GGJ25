using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBubble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sharp")
            Pop();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Freeze")
            GameManager.instance.TransformBubble(gameObject, GameManager.instance.frozenBubble);
    }

    protected void Pop()
    {
        GetComponent<Animator>().SetTrigger("Pop");
        Destroy(gameObject, 0.2f);
    }
}
