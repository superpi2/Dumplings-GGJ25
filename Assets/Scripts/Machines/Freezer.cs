using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour
{
    public KeyCode toggleFreezer;

    public SpriteRenderer machineSprite;
    public GameObject auraField;
    float auraScale;

    public Sprite[] sprites;

    private void Start()
    {
        auraScale = 0f;
        ScaleAura(0);
    }

    private void Update()
    {
        if (KeyManager.instance.IsMachineOn(toggleFreezer))
        {
            machineSprite.sprite = sprites[1];
            auraScale += 10f * Time.deltaTime;
        } else
        {
            machineSprite.sprite = sprites[0];
            auraScale -= 10f * Time.deltaTime;
        }

        auraScale = Mathf.Clamp(auraScale, 0f, 6f);
        ScaleAura(auraScale);

        Vector3 auraRot = auraField.transform.rotation.eulerAngles;
        auraRot.z += Time.deltaTime * 30f;
        auraField.transform.rotation = Quaternion.Euler(auraRot);
    }

    void ScaleAura(float scale)
    {
        if (scale <= 0)
        {
            auraField.SetActive(false);
            return;
        }

        auraField.SetActive(true);
        auraField.transform.localScale = new Vector3(scale, scale, 1f);
    }
}
