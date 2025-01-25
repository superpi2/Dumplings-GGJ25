using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMachine : MonoBehaviour
{
    public KeyCode moveToggle;
    public KeyCode fire;

    public Vector2[] path;
    int currentTarget;

    public GameObject dartPrefab;
    public float force;
    float cooldown = 0f;

    private void Start()
    {
        currentTarget = 0;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (KeyManager.instance.IsMachineOn(moveToggle))
        {
            transform.position = Vector3.MoveTowards(transform.position, path[currentTarget], 5f * Time.deltaTime);

            if (Vector3.Distance(transform.position, path[currentTarget]) < 0.01f)
                currentTarget = (currentTarget + 1) % path.Length;
        }

        if (Input.GetKeyDown(fire) && cooldown <= 0)
        {
            cooldown = 0.25f;

            GameObject proj = Instantiate(dartPrefab, transform.position, transform.rotation);
            proj.GetComponent<Rigidbody2D>().AddForce(force * transform.up, ForceMode2D.Impulse);
            Destroy(proj, 5f);
        }
    }
}
