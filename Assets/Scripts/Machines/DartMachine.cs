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
    public GameObject dartDisplay;

    public float force;
    public float delay = 0.25f;

    public string sound;

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

        if (KeyManager.instance.IsMachineFired(fire) && cooldown <= 0)
        {
            cooldown = delay;
            AudioManager.instance.PlaySFX(sound);

            GameObject proj = Instantiate(dartPrefab, transform.position, transform.rotation);
            proj.GetComponent<Rigidbody2D>().AddForce(force * transform.up, ForceMode2D.Impulse);
            Destroy(proj, 8f);
        }

        if (dartDisplay != null)
        {
            dartDisplay.SetActive(cooldown < 0f);
        }
    }
}
