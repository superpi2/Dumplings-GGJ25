using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int level;
    public bool startingSelection;

    private void Start()
    {
        if (level > GameManager.instance.levelsCleared + 1)
        {
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<TMP_Text>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    void Update()
    {
        if (startingSelection && EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnClick()
    {
        GameManager.currentStage = level;
        SceneFader.instance.FadeToScene("Stage" + level);
    }
}
