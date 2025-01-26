using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animator;

    public float targetVelocity;

    float freezeAmount;
    int freezeZones;

    bool alive;
    bool grounded;
    bool wallLeft;
    bool wallRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        alive = true;
        freezeAmount = 0f;
    }

    private void Start()
    {
        rb.velocity = new Vector2(2f, 0f);
    }

    private void Update()
    {
        rb.velocity = new Vector2(targetVelocity, rb.velocity.y);

        if (!alive)
            return;

        PhysicsLoop();

        if (wallLeft)
            targetVelocity = Mathf.Abs(targetVelocity);
        else if (wallRight)
            targetVelocity = -Mathf.Abs(targetVelocity);

        sprite.flipX = targetVelocity < 0f;
        animator.SetBool("Falling", !grounded);

        // freezing
        if (freezeZones == 0)
            freezeAmount -= Time.deltaTime;
        else
            freezeAmount += Time.deltaTime / 2f;

        freezeAmount = Mathf.Clamp01(freezeAmount);

        if (freezeAmount >= 1f)
            PlayerDies();

        sprite.color = new Color(1f - Mathf.Pow(freezeAmount, 2f), 1f, 1f, 1f);
    }

    void PhysicsLoop()
    {
        Vector2 origin = (Vector2)transform.position + new Vector2(0f, -0.6f);
        Vector2 size = new Vector2(0.6f, 1 / 32f);

        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            origin, size, 0f, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(origin - size / 2f, size, Color.red);

        grounded = hits.Length > 0;

        origin = (Vector2)transform.position + new Vector2(-0.3f, 0f);
        size = new Vector2(1 / 32f, 0.75f);

        wallLeft = false;

        hits = Physics2D.BoxCastAll(
            origin, size, 0f, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(origin - size / 2f, size, Color.green);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && !hit.collider.usedByEffector)
            {
                wallLeft = true;
                break;
            }
        }

        wallRight = false;

        origin = (Vector2)transform.position + new Vector2(0.3f, 0f);

        hits = Physics2D.BoxCastAll(
            origin, size, 0f, Vector2.down, 0f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(origin - size / 2f, size, Color.green);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && !hit.collider.usedByEffector)
            {
                wallRight = true;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alive)
            return;

        if (collision.gameObject.tag == "Goal")
        {
            GameManager.instance.StageClear();
        }

        if (collision.gameObject.tag == "Sharp")
        {
            PlayerDies();
        }

        if (collision.gameObject.tag == "Freeze")
            freezeZones += 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!alive)
            return;

        if (collision.gameObject.tag == "Freeze")
            freezeZones -= 1;
    }

    public bool EnterBubble(GameObject bubble)
    {
        if (!alive || rb.isKinematic)
            return false;

        transform.SetParent(bubble.transform);
        transform.localPosition = Vector3.zero;
        rb.isKinematic = true;
        return true;
    }

    public void ExitBubble()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
    }

    void PlayerDies()
    {
        alive = false;
        targetVelocity = 0f;
        sprite.color = Color.white;

        animator.SetBool("Frozen", freezeAmount >= 1f);
        animator.SetTrigger("Die");
    }
}
