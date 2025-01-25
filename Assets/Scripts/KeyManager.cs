using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;

    public KeyCode[] keys;
    Dictionary<KeyCode, bool> keyStates;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        keyStates = new Dictionary<KeyCode, bool>();

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

    public bool IsMachineOn(KeyCode key)
    {
        return keyStates.TryGetValue(key, out bool r) && r;
    }
}
