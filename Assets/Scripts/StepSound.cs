using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour
{
    public void PlaySound()
    {
        AudioManager.instance.PlaySFX("cloudStep");
    }
}
