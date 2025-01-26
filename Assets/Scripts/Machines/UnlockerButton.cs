using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockerButton : MonoBehaviour
{
    public GameObject indicator;
    public KeyCode unlockedKey;

    bool active;

    private void Start()
    {
        active = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active) return;

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "FrozenBubble")
        {
            active = false;
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Unlocked());
        }   
    }

    IEnumerator Unlocked()
    {
        KeyManager.instance.AddKey(unlockedKey);
        GetComponent<SpriteRenderer>().enabled = false;

        yield return null;

        for (float t = 0f; t < 1f; t += Time.deltaTime * 4f)
        {
            yield return null;
            indicator.transform.localScale = Vector3.one * t * 1.2f;
        }

        indicator.transform.localScale = Vector3.one * 1.2f;
    }
}
