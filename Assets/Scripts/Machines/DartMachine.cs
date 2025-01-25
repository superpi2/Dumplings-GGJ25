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

    private void Start()
    {
        currentTarget = 0;
    }

    void Update()
    {
        if (KeyManager.instance.IsMachineOn(moveToggle))
        {
            transform.position = Vector3.MoveTowards(transform.position, path[currentTarget], 5f * Time.deltaTime);

            if (Vector3.Distance(transform.position, path[currentTarget]) < 0.01f)
                currentTarget = (currentTarget + 1) % path.Length;
        }
    }
}
