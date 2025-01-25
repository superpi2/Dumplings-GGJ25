using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockerButton : MonoBehaviour
{
    public GameObject indicator;
    public KeyCode unlockedKey;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "FrozenBubble")
        {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Unlocked());
        }   
    }

    IEnumerator Unlocked()
    {
        KeyManager.instance.AddKey(unlockedKey);

        yield return null;

        for (float t = 0f; t < 1f; t += Time.deltaTime)
        {
            yield return null;
            indicator.transform.localScale = Vector3.one * t * 1.2f;
        }

        indicator.transform.localScale = Vector3.one * 1.2f;
    }
}
