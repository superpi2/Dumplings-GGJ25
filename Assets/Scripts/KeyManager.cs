using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;

    public List<string> keys;
    Dictionary<string, bool> keyStates;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        keyStates = new Dictionary<string, bool>();

        foreach (string key in keys)
        {
            keyStates.Add(key, false);
        }
    }

    private void Update()
    {
        foreach (string key in keys)
        {
            if (Input.GetButtonDown(key))
            {
                keyStates[key] = !keyStates[key];
            }
        }
    }

    public void AddKey(string key)
    {
        keys.Add(key);
        keyStates.Add(key, false);
    }

    public bool IsMachineOn(string key)
    {
        return !PauseMenu.instance.paused && keyStates.TryGetValue(key, out bool r) && r;
    }

    public bool IsMachineFired(string key)
    {
        return !PauseMenu.instance.paused && keyStates.TryGetValue(key, out bool _) && Input.GetButtonDown(key);
    }
}
