using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public KeyCode[] keys;
    Dictionary<KeyCode, bool> keyStates;

    private void Start()
    {
        foreach (KeyCode key in keys)
        {
            keyStates.Add(key, false);
        }
    }

    private void Update()
    {
        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                keyStates[key] = !keyStates[key];
            }
        }
    }
}
