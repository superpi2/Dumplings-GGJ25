using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public KeyCode moveToggle;

    public Vector2[] path;
    int currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = path[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, path[currentTarget], 10f * Time.deltaTime);

        currentTarget = KeyManager.instance.IsMachineOn(moveToggle) ? 1 : 0;
    }
}
