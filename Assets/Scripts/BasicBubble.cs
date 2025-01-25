using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBubble : MonoBehaviour
{
    float freezeAmount;
    int freezeZones;

    public SpriteRenderer freezeOverlay;

    private void Start()
    {
        freezeAmount = 0f;
        freezeZones = 0;
    }

    private void Update()
    {
        if (freezeZones == 0)
            freezeAmount -= Time.deltaTime;
        else
            freezeAmount += Time.deltaTime;

        freezeAmount = Mathf.Clamp01(freezeAmount);

        if (freezeAmount >= 1f)
            GameManager.instance.TransformBubble(gameObject, GameManager.instance.frozenBubble);

        freezeOverlay.color = new Color(1f, 1f, 1f, Mathf.Pow(freezeAmount, 2f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.gameObject.tag == "Sharp")
            Pop();

        if (collision.gameObject.tag == "Freeze")
            freezeZones += 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.gameObject.tag == "Freeze")
            freezeZones -= 1;
    }

    protected void Pop()
    {
        GetComponent<Animator>().SetTrigger("Pop");
        Destroy(gameObject, 0.2f);
    }
}
